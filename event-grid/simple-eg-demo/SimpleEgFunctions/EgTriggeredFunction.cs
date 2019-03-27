// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName=EgTriggeredFunction
// ngrok http -host-header=localhost 7071

// https://65e3143a.ngrok.io/runtime/webhooks/EventGrid?functionName=EgTriggeredFunction

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace SimpleEgFunctions
{
    public static class EgTriggeredFunction
    {
        [FunctionName("EgTriggeredFunction")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.Data.ToString());
        }
    }
}
