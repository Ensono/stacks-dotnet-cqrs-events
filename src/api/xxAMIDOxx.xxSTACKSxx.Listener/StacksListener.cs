using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.Listener
{
    public class StacksListener
    {
        private readonly ILogger<StacksListener> logger;

        public StacksListener(ILogger<StacksListener> logger)
        {
            this.logger = logger;
        }

        [FunctionName("StacksListener")]
        public void Run([ServiceBusTrigger(
            "%TOPIC%",
            "%TOPIC_SUBSCRIPTION%",
            Connection = "SERVICEBUS_CONNECTIONSTRING")] string mySbMsg)
        {
            logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
