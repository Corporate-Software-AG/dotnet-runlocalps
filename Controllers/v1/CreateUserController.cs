﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using RunLocalPowershell.Models;
using System.Reflection;
using Microsoft.PowerShell;

namespace RunLocalPowershell.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CreateUserController : ControllerBase
    {
        private readonly ILogger<CreateUserController> _logger;

        public CreateUserController(ILogger<CreateUserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public string Post([FromBody] User user)
        {
            var scriptParams = new Dictionary<string, string>();
            PropertyInfo[] properties = typeof(User).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(user) != null)
                {
                    scriptParams.Add(property.Name, property.GetValue(user).ToString());
                }
                else
                {
                    scriptParams.Add(property.Name, "");
                }
            }

            return RunScript(scriptParams);
        }

        private string RunScript(Dictionary<string, string> scriptParams)
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
                ? "NO OUTPUT"
                : $"Run  {Environment.GetEnvironmentVariable("ScriptPath")} with OUTPUT: \n\n{outputString}";

            return responseMessage;
        }
    }
}
