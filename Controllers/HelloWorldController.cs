using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class HelloWorldController: ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;

        IHelloWorldService helloWorldService;

        TareasContext dbcontext;

        public HelloWorldController(IHelloWorldService helloWorld, ILogger<HelloWorldController> logger, TareasContext db)
        {
            helloWorldService = helloWorld;
            _logger = logger;
            dbcontext = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Retornando la lista de HeloWorldConroller");
            return Ok(helloWorldService.GetHelloWorld());
        }

        [HttpGet]
        [Route("createdb")]
        public IActionResult CreateDatabase()
        {
            dbcontext.Database.EnsureCreated();
            return Ok();
        }
    }