stacks-dotnet-cqrs-events

### Folders of interest in this repository

```
stacks-dotnet-cqrs-events
│   README.md
└─── src
│   └─── api
│	│    └─── xxAMIDOxx.xxSTACKSxx.API
│	│    └─── other API related projects
│	│
│   └─── functions
│	│	 └─── xxAMIDOxx.xxSTACKSxx.Listner
│	│    └─── xxAMIDOxx.xxSTACKSxx.Worker
│	│
│   └─── tests
│	│
│   └─── worker
│        └─── xxAMIDOxx.xxSTACKSxx.BackgroundWorker
```

- The `api` folder contains everything related to the API and is a standalone executable
- The `functions` folder contains two Azure Functions
	- `Listener` is an Azure Service Bus subscription (filtered) trigger that listens for `MenuCreatedEvent`
	- `Worker` is a CosmosDB change feed trigger function that publishes a `CosmosDbChangeFeedEvent` when a new entity has been added or was changed to CosmosDB
- The `worker` folder contains a background worker that listens to all event types from the ASB topic and shows example handlers for them and the use of the [Amido.Stacks.Messaging.Azure.ServiceBus](https://github.com/amido/stacks-dotnet-packages-messaging-asb) package.

The API, functions and worker all depend on the [Amido.Stacks.Messaging.Azure.ServiceBus](https://github.com/amido/stacks-dotnet-packages-messaging-asb) package for their communication with ASB.

The functions and workers are all stand-alone implementations that can be used together or separately in different projects.

### Running the API locally on MacOS

To run the API locally on MacOS there are a couple of prerequisites that you have to be aware of. You'll need a CosmosDB emulator and access to Azure Service Bus.

#### Docker CosmosDB emulator setup

1. Get Docker from here - https://docs.docker.com/docker-for-mac/install/
2. Follow the instructions outlined here - https://docs.microsoft.com/en-us/azure/cosmos-db/linux-emulator?tabs=ssl-netstd21
3. From the CosmosDB UI create a database called `Stacks`.
4. Inside the `Stacks` database create a container called `Menu`

#### Azure Service Bus

You'll need an Azure Service Bus namespace and a topic with subscriber in order to be able to publish application events.

#### Configuring CosmosDB and ServiceBus

Now that you have your CosmosDB all set, you can point the API project to it. In `appsettings.json` you can see the following sections

```json
"CosmosDb": {
    "DatabaseAccountUri": "https://localhost:8081/",
    "DatabaseName": "Stacks",
    "SecurityKeySecret": {
        "Identifier": "COSMOSDB_KEY",
        "Source": "Environment"
    }
},
"ServiceBusConfiguration": {
    "Sender": {
        "Topics": [
            {
                "Name": "servicebus-topic-lius",
                "ConnectionStringSecret": {
                    "Identifier": "SERVICEBUS_CONNECTIONSTRING",
                    "Source": "Environment"
                }
            }
        ]
    }
}
```

The `SecurityKeySecret` and `ConnectionStringSecret` sections are needed because of our use of the `Amido.Stacks.Configuration` package. `COSMOSDB_KEY` and `SERVICEBUS_CONNECTIONSTRING` have to be set before you can run the project. If you want to debug the solution with VSCode you usually have a `launch.json` file. In that file there's an `env` section where you can put environment variables for the current session.

```json
"env": {
    "ASPNETCORE_ENVIRONMENT": "Development",
    "COSMOSDB_KEY": "YOUR_COSMOSDB_PRIMARY_KEY",
    "SERVICEBUS_CONNECTIONSTRING": "YOUR_SERVICE_BUS_CONNECTION_STRING"
}
```

If you want to run the application without VSCode you'll have to set the `COSMOSDB_KEY` and `SERVICEBUS_CONNECTIONSTRING` environment variables through your terminal.

```shell
export COSMOSDB_KEY=YOUR_COSMOSDB_PRIMARY_KEY
export SERVICEBUS_CONNECTIONSTRING=YOUR_SERVICE_BUS_CONNECTION_STRING
```

This will set the environment variables only for the current session of your terminal.

To set the environment variables permanently on your system you'll have to edit your `bash_profile` or `.zshenv` depending on which shell are you using.

```shell
# Example for setting env variable in .zchenv
echo 'export COSMOSDB_KEY=YOUR_COSMOSDB_PRIMARY_KEY' >> ~/.zshenv
echo 'export SERVICEBUS_CONNECTIONSTRING=YOUR_SERVICE_BUS_CONNECTION_STRING' >> ~/.zshenv
```

### Running the Worker ChangeFeed listener locally

Running the Worker function locally is pretty straightforward. You'll have to set the following environment variables in your `local.settings.json` file

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "SERVICEBUS_CONNECTIONSTRING": "SERVICE_BUS_CONNECTION_STRING",
        "DatabaseName": "Stacks",
        "CollectionName": "Menu",
        "LeaseCollectionName": "Leases",
        "CosmosDbConnectionString": "COSMOS_DB_CONNECTION_STRING",
        "CreateLeaseCollectionIfNotExists": true
    }
}
```
