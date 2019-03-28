using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;

namespace EventGridPublisher
{
    public class PersonHiredEventData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            // from Event Grid Topic | Topic Endpoint
            var topicEndpoint = "https://person-topic.westeurope-1.eventgrid.azure.net/api/events";

            // from Event Grid Topic | Access Key
            var topicKey = "hYxOc2m5tlgndK6VTBAS3oY5P6tPDLpelf9getneoJ4=";

            var topicHostname = new Uri(topicEndpoint).Host;
            var topicCredentials = new TopicCredentials(topicKey);
            var client = new EventGridClient(topicCredentials);

            var events = GetEventsList();
            await client.PublishEventsAsync(topicHostname, events);
            Console.WriteLine("Yep, we're done!");
            Console.ReadLine();
        }

        static IList<EventGridEvent> GetEventsList()
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>();

            for (int i = 0; i < 10; i++)
            {
                eventsList.Add(new EventGridEvent
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "AzureDay.PersonHired",
                    Data = new PersonHiredEventData
                    {
                        Name = "Jakub",
                        Surname = "Gutkowski"
                    },
                    EventTime = DateTime.Now,
                    Subject = "Hiring",
                    DataVersion = "2.0"
                });
            }

            return eventsList;
        }
    }
}

