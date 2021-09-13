using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amido.Stacks.Application.CQRS.Events;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace xxAMIDOxx.xxSTACKSxx.Listener
{
    // TODO: Move this to https://github.com/amido/stacks-dotnet-packages-messaging-aeh
    public interface IMessageReader
    {
        T Read<T>(EventData eventData);
    }

    // TODO: Move this to https://github.com/amido/stacks-dotnet-packages-messaging-aeh
    public class JsonMessageSerializer : IMessageReader
    {
        public T Read<T>(EventData eventData)
        {
            var jsonBody = Encoding.UTF8.GetString(eventData.Body);
            return JsonConvert.DeserializeObject<T>(jsonBody);
        }
    }

    public class StacksListener
    {
        private readonly IMessageReader msgReader;
        private readonly ILogger<StacksListener> logger;

        public StacksListener(IMessageReader msgReader, ILogger<StacksListener> logger)
        {
            this.msgReader = msgReader;
            this.logger = logger;
        }

        [FunctionName("StacksListener")]
        public void Run([EventHubTrigger(
            "%EVENTHUB_NAME%",
            Connection = "EVENTHUB_CONNECTIONSTRING")] EventData[] events)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    var appEvent = msgReader.Read<MenuCreatedEvent>(eventData);
                    logger.LogInformation($"Message read. Menu Id: {appEvent?.MenuId}");
                    logger.LogInformation($"C# Event Hub trigger function processed an event: {appEvent}");
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
