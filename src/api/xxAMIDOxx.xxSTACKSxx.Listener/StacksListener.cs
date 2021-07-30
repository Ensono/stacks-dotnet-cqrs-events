using System;
using System.Text;
using Amido.Stacks.Application.CQRS.Events;
using Amido.Stacks.Messaging.Azure.ServiceBus.Serializers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.Listener
{
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
        public void Run([ServiceBusTrigger(
            "%TOPIC%",
            "%TOPIC_SUBSCRIPTION%",
            Connection = "SERVICEBUS_CONNECTIONSTRING")] Message mySbMsg)
        {
            var appEvent = msgReader.Read<StacksCloudEvent<MenuCreatedEvent>>(mySbMsg);

            // TODO: Log the data field of appEvent
            logger.LogInformation($"C# ServiceBus topic trigger function processed message: {appEvent}");
        }
    }
}
