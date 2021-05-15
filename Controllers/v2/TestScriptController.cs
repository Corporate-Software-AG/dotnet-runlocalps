using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.PowerShell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading.Tasks;

namespace RunLocalPowershell.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class TestScriptController : ControllerBase
    {
        private readonly ILogger<CreateUserController> _logger;

        public TestScriptController(ILogger<CreateUserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {

            return RunScript();
        }

        private string RunScript()
        {
            // Create a default initial session state and set the execution policy.
            InitialSessionState initialSessionState = InitialSessionState.CreateDefault();
            initialSessionState.ExecutionPolicy = ExecutionPolicy.Unrestricted;

            // Create a runspace and open it. This example uses C#8 simplified using statements
            using Runspace runspace = RunspaceFactory.CreateRunspace(initialSessionState);

            runspace.Open();

            string outputString = "";

            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;

                string scriptPath = Environment.GetEnvironmentVariable("ScriptPath");
                // specify the script code to run.
                ps.AddScript(scriptPath);

                // execute the script and await the result.                
                var output = ps.Invoke();

                // print the resulting pipeline objects to the console.


                foreach (var item in output)
                {
                    Console.WriteLine(item.ToString());
                    outputString += item.ToString() + "\n";
                }
            }
            runspace.Close();

            string responseMessage = string.IsNullOrEmpty(outputString)
                ? $"NO OUTPUT in {Environment.GetEnvironmentVariable("ScriptPath")}"
                : $"Connect to {Environment.GetEnvironmentVariable("ScriptPath")} with OUTPUT: \n\n{outputString}";

            return responseMessage;

        }
    }
}
