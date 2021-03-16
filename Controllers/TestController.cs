using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RunLocalPowershell.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<CreateUserController> _logger;

        public TestController(ILogger<CreateUserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {

            return "Hello World!";
        }
    }
}
