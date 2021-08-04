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
            databaseName: "%COSMOSDB_DATABASE_NAME%",
            collectionName: "%COSMOSDB_COLLECTION_NAME%",
            ConnectionStringSetting = "COSMOSDB_CONNECTIONSTRING",
            LeaseCollectionName = "%COSMOSDB_LEASE_COLLECTION_NAME%",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input)
        {
            if (input != null && input.Count > 0)
            {
                logger.LogInformation("Documents modified " + input.Count);
                foreach(var changedItem in input)
                { 
                    logger.LogInformation("Document read. Id: " + changedItem.Id);

                    // TODO: Raising a ItemChangedEvent for demo purposes!
                    var itemChangedEvent = new ItemChangedEvent(
                        1, Guid.NewGuid(), 
                        changedItem.Id,
                        changedItem.ETag);

                    appEventPublisher.PublishAsync(itemChangedEvent);
                }
            }
        }
    }
}
