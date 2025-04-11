using Dal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            this._context.Database.EnsureCreated();

            return Ok("Database created");
        }


        [HttpGet("destroy")]
        public IActionResult Destroy()
        {
            this._context.Database.EnsureDeleted();

            return Ok("Database deleted");
        }
    }
}
