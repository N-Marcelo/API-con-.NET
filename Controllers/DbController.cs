using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class DBController: ControllerBase
    {
        TareasContext dbcontext;

        public DBController(TareasContext db)
        {
            dbcontext = db;
        }
        
        [HttpGet]
        [Route("createdb")]
        public IActionResult CreateDatabase()
        {
            dbcontext.Database.EnsureCreated();
            return Ok();
        }
    }