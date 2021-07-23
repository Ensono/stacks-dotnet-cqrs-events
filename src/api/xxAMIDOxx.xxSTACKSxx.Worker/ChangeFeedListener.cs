using System;
using System.Collections.Generic;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.Worker
{
    public class ChangeFeedListener
    {
        private readonly IApplicationEventPublisher appEventPublisher;
        private readonly ILogger<ChangeFeedListener> logger;

        public ChangeFeedListener(
            IApplicationEventPublisher appEventPublisher,
            ILogger<ChangeFeedListener> logger)
        {
            this.appEventPublisher = appEventPublisher;
            this.logger = logger;
        }

        [FunctionName(Constants.FunctionNames.CosmosDbChangeFeedListener)]
        public void Run([CosmosDBTrigger(
            databaseName: "%DatabaseName%",
            collectionName: "%CollectionName%",
            ConnectionStringSetting = "CosmosDbConnectionString",
            LeaseCollectionName = "%LeaseCollectionName%",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input)
        {
            if (input != null && input.Count > 0)
            {
                logger.LogInformation("Documents modified " + input.Count);
                logger.LogInformation("First document Id " + input[0].Id);

                // TODO: This event is here for demo purposes!
                appEventPublisher.PublishAsync(new ItemChangedEvent(1, Guid.NewGuid()));
            }
        }
    }
}
