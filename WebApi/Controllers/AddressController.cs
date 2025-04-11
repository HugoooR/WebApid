using Dal;
using Exercice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private ApplicationDbContext context { get; set; }

        public AddressController (ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            var list = this.context.Addresses.ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetAddress([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            Address? address = this.context.Addresses.Find(id);

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public IActionResult AddAddress([FromBody] Address address)
        {
            this.context.Addresses.Add(address);
            this.context.SaveChanges();

            return Created($"address/{address.AddressID}", address);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveAddress([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            Address? address = this.context.Addresses.Find(id);
            if (address == null)
                return NotFound();

            this.context.Addresses.Remove(address);
            this.context.SaveChanges();

            return Ok(address);
        }

        [HttpPut("{id}")]
        public IActionResult EditAddress([FromRoute] int id, [FromBody] Address address)
        {
            if (id != address.AddressID)
                return BadRequest();

            if (!this.context.Addresses.Any(a => a.AddressID== id))
                return NotFound();

            this.context.Addresses.Update(address);
            this.context.SaveChanges();

            return Ok(address);
        }

    }
}
