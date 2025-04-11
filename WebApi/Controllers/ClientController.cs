using Dal;
using Exercice;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        
        private  ApplicationDbContext context { get; set; }

        public ClientController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            var list = this.context.Clients.ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetClient([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            Client? client = this.context.Clients.Find(id);

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public IActionResult AddClient([FromBody] Client client)
        {
            this.context.Clients.Add(client);
            this.context.SaveChanges();

            return Created($"client/{client.ClientID}", client);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveClient([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            Client? client = this.context.Clients.Find(id);
            if (client == null)
                return NotFound();

            this.context.Clients.Remove(client);
            this.context.SaveChanges();

            return Ok(client);
        }
    }
}
