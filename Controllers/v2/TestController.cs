using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RunLocalPowershell.Controllers.v2
{
    [Route("api/v2/[controller]")]
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
