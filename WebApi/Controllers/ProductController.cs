using Dal;
using Exercice;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {

        private ApplicationDbContext context { get; set; }

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var list = this.context.Products.ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            Product? product = this.context.Products.Find(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("search")]
        public IActionResult SearchProducts([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term is required");
        
            var lowerTerm = term.ToLower();
        
            var results = context.Products
                .Where(p => 
                    p.Name.ToLower().Contains(lowerTerm) ||
                    p.Description.ToLower().Contains(lowerTerm) ||
                    p.ProductID.ToString() == term
                )
                .ToList();
        
            return Ok(results);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();

            return Created($"product/{product.ProductID}", product);
        }

        [HttpPut("{id}")]
        public IActionResult EditProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (id != product.ProductID)
                return BadRequest();

            if (!this.context.Products.Any(p => p.ProductID == id))
                return NotFound();

            this.context.Products.Update(product);
            this.context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveProduct([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            Product? product = this.context.Products.Find(id);
            if (product == null)
                return NotFound();

            this.context.Products.Remove(product);
            this.context.SaveChanges();

            return Ok(product);
        }
    }
}
