using Dal;
using Exercice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext context;

        public OrderController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = context.Orders
                .Include(o => o.OrderProducts)
                .Select(o => new OrderDto
                {
                    Date = o.Date,
                    ClientId = o.ClientId,
                    OrderProducts = o.OrderProducts.Select(op => new OrderProductDto
                    {
                        ProductId = op.ProductId,
                        Quantity = op.Quantity
                    }).ToList()
                }).ToList();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();

            var dto = new OrderDto
            {
                Date = order.Date,
                ClientId = order.ClientId,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity
                }).ToList()
            };

            return Ok(dto);
        }
        [HttpPost]
        public IActionResult Post([FromBody] OrderDto dto)
        {
            var clientExists = context.Clients.Any(c => c.ClientID == dto.ClientId);
            if (!clientExists)
            {
                return NotFound($"Le client avec l'ID {dto.ClientId} n'existe pas.");
            }

            var order = new Order
            {
                Date = dto.Date,
                ClientId = dto.ClientId,
                OrderProducts = dto.OrderProducts.Select(op => new OrderProduct
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity
                }).ToList()
            };

            context.Orders.Add(order);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, dto);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderDto dto)
        {
            var order = context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();

            order.Date = dto.Date;
            order.ClientId = dto.ClientId;
            context.OrderProducts.RemoveRange(order.OrderProducts);

            order.OrderProducts = dto.OrderProducts.Select(op => new OrderProduct
            {
                ProductId = op.ProductId,
                Quantity = op.Quantity
            }).ToList();

            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();

            context.OrderProducts.RemoveRange(order.OrderProducts);
            context.Orders.Remove(order);
            context.SaveChanges();
            return NoContent();
        }
    }
}
