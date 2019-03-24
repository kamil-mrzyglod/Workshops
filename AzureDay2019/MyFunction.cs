using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AzureDay2019.Triggers;
using LinqToTwitter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureDay2019
{
    public static class MyFunction
    {
        [FunctionName("MyFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [QueryStringBinding] IDictionary<string, string> parameters,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        [FunctionName("MyTwitterFunction")]
        public static async Task Run2(
            [TwitterTrigger] List<Status> statuses,
            ILogger log)
        {
            log.LogInformation("Function triggered via Twitter.");

            foreach (var status in statuses)
            {
                log.LogInformation($"Tweet status: {status.Text}");
            }
           
        }
    }
}
