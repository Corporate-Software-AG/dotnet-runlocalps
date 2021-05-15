using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using RunLocalPowershell.Models;
using System.Reflection;
using Microsoft.PowerShell;
using System.Net.Http;
using System.Net;

namespace RunLocalPowershell.Controllers.v2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class CreateUserController : ControllerBase
    {
        private readonly ILogger<CreateUserController> _logger;

        public CreateUserController(ILogger<CreateUserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public object Post([FromBody] User user)
        {
            var scriptParams = new Dictionary<string, string>();
            PropertyInfo[] properties = typeof(User).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(user) != null)
                {
                    scriptParams.Add(property.Name, property.GetValue(user).ToString());
                }
            }

            return RunScript(scriptParams);
        }

        private object RunScript(Dictionary<string, string> scriptParams)
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
                ps.AddCommand(scriptPath);

                // specify the parameters to pass into the script.
                ps.AddParameters(scriptParams);

                // execute the script and await the result.              
                try
                {
                    var result = ps.Invoke();

                    if (ps.HadErrors)
                    {
                        foreach (ErrorRecord error in ps.Streams.Error.ReadAll())
                        {
                            Console.WriteLine(error.ToString());
                            throw new ApplicationFailedException(error.ErrorDetails.Message);
                        }
                    }

                    foreach (var item in result)
                    {
                        outputString += item.ToString() + "\n";
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationFailedException(ex.Message);
                }
                finally
                {
                    ps.Stop();
                }


            }
            runspace.Close();

            string responseMessage = string.IsNullOrEmpty(outputString) ? "NO OUTPUT" : outputString;

            var obj = new
            {
                ScriptPath = Environment.GetEnvironmentVariable("ScriptPath"),
                ScriptOutput = responseMessage,
                Status = "Success"
            };

            return obj;
        }
    }
}
