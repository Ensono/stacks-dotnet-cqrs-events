# stacks-dotnet-cqrs-events

The full documentation on Amido Stacks can be found [here](https://amido.github.io/stacks/).

Amido Stacks targets different cloud providers.

[Azure](https://amido.github.io/stacks/docs/workloads/azure/backend/netcore/introduction_netcore)

### Folders of interest in this repository

```shell
stacks-dotnet-cqrs-events
│   README.md
└─── src
│    └─── api
│    │    └─── xxAMIDOxx.xxSTACKSxx.API
│    │    └─── other API related projects
│    └─── functions
│    │    └─── func-aeh-listener
│    │         └─── xxAMIDOxx.xxSTACKSxx.Listener
│    │    └─── func-asb-listener
│    │         └─── xxAMIDOxx.xxSTACKSxx.Listener
│    │    └─── func-cosmosdb-worker
│    │         └─── xxAMIDOxx.xxSTACKSxx.Worker
│    └─── tests
│    └─── worker
│         └─── xxAMIDOxx.xxSTACKSxx.BackgroundWorker
```

- The `api` folder contains everything related to the API and is a standalone executable
- The `functions` folder contains 3 sub-folders with Azure Functions solutions
    - `func-aeh-listener` is an Azure Event Hub trigger that listens for `MenuCreatedEvent`
    - `func-asb-listener` is an Azure Service Bus subscription (filtered) trigger that listens for `MenuCreatedEvent`
    - `func-cosmosdb-worker` is a CosmosDB change feed trigger function that publishes a `CosmosDbChangeFeedEvent` when a new entity has been added or was changed to CosmosDB
- The `worker` folder contains a background worker that listens to all event types from the ASB topic and shows example handlers for them and the use of the [Amido.Stacks.Messaging.Azure.ServiceBus](https://github.com/amido/stacks-dotnet-packages-messaging-asb) package.

The API, functions and worker all depend on the [Amido.Stacks.Messaging.Azure.ServiceBus](https://github.com/amido/stacks-dotnet-packages-messaging-asb) and the [Amido.Stacks.Messaging.Azure.EventHub](https://github.com/amido/stacks-dotnet-packages-messaging-aeh) packages for their communication with Azure Service Bus or Azure Event Hub depending on the specific implementation.

The functions and workers are all stand-alone implementations that can be used together or separately in different projects.

### Templates

All templates from this repository come as part of the [Amido.Stacks.CQRS.Events.Templates](https://www.nuget.org/packages/Amido.Stacks.CQRS.Templates/) NuGet package. The list of templates inside the package are as follows:

- `stacks-cqrs-events-app`. The full template including source + build infrastructure.
- `stacks-cqrs-events-webapi`. A template for the `api` project. If you need a CQRS WebAPI that can publish messages to ServiceBus, this is the template to use.
- `stacks-asb-worker`. This template contains a background worker application that reads and handles messages from a ServiceBus subscription.
- `stacks-az-func-asb-listener`. Template containing an Azure Function project with a single function that has a Service Bus subscription trigger. The function receives the message and deserializes it.
- `stacks-az-func-aeh-listener`. Template containing an Azure Function project with a single function that has a Event Hub trigger. The function receives the message and deserializes it.
- `stacks-az-func-cosmosdb-worker`. Azure Function containing a CosmosDb change feed trigger. Upon a CosmosDb event, the worker reads it and publishes a message to Service Bus.

### Template usage

#### Template installation

For the latest template version, please consult the Nuget page [Amido.Stacks.CQRS.Events.Templates](https://www.nuget.org/packages/Amido.Stacks.CQRS.Events.Templates/). To install the templates to your machine via the command line:

```shell
dotnet new --install Amido.Stacks.CQRS.Events.Templates
```

The output you'll see will list all installed templates (not listed for brevity). In that list you'll see the just installed Amido Stacks templates

```shell
Template Name                                    Short Name                       Language    Tags
-----------------------------------------------  -------------------------------  ----------  ------------------------------------------
...
Amido Stacks Azure Function CosmosDb Worker      stacks-az-func-cosmosdb-worker   [C#]        Stacks/Azure Function/CosmosDb/Worker
Amido Stacks Azure Function Service Bus Trigger  stacks-az-func-asb-listener      [C#]        Stacks/Azure Function/Service Bus/Listener
Amido Stacks Azure Function Event Hub Trigger    stacks-az-func-aeh-listener      [C#]        Stacks/Azure Function/Event Hub/Listener
Amido Stacks Service Bus Worker                  stacks-asb-worker                [C#]        Stacks/Service Bus/Worker
Amido Stacks CQRS Events Web API                 stacks-cqrs-events-webapi        [C#]        Stacks/CQRS/Events/WebAPI
Amido Stacks CQRS Events App                     stacks-cqrs-events-app           [C#]        Stacks/Application/Infrastructure/CQRS/Events/WebAPI
...

Examples:
    dotnet new mvc --auth Individual
    dotnet new react --auth Individual
    dotnet new --help
    dotnet new stacks-az-func-asb-listener --help
```

#### Uninstalling a template

To uninstall the template pack you have to execute the following command

```shell
dotnet new --uninstall Amido.Stacks.CQRS.Events.Templates
```

#### Important parameters

- **-n|--name**
  - Sets the project name
  - Omitting it will result in the project name being the same as the folder where the command has been ran from
- **-do|--domain**
  - Sets the name of the aggregate root object. It is also the name of the collection within CosmosDB instance.
- **-db|--database**
  - Configures which database provider to be used
- **-e|--eventPublisher**
  - Configures the messaging service
- **-e:fw|--enableFunctionWorker**
  - Configures the messaging service
- **-e:fl|--enableFunctionListener**
  - Configures the messaging service
- **-e:bw|--enableBackgroundWorker**
  - Configures the messaging service
- **-o|--output**
  - Sets the path to where the project is added
  - Omitting the parameter will result in the creation of a new folder

#### Creating a new WebAPI + CQRS + Events project from the template

Let's say you want to create a brand new WebAPI with CQRS and Event sourcing for your project.

It's entirely up to you where you want to generate the WebAPI. For example your company has the name structure `Foo.Bar` as a prefix to all your namespaces where `Foo` is the company name and `Bar` is the name of the project. If you want the WebAPI to have a domain `Warehouse`, use `CosmosDb`, publish events to `ServiceBus` and be generated inside a folder called `new-proj-folder` you'll execute the following command:

```shell
% dotnet new stacks-cqrs-events-app -n Foo.Bar -do Warehouse -db CosmosDb -e ServiceBus -o new-proj-folder
The template "Amido Stacks CQRS Events App" was created successfully.
```

#### Adding a function template to your project

Let's say you want to add either `stacks-az-func-cosmosdb-worker` or `stacks-az-func-asb-listener` function apps to your solution or project.

It's entirely up to you where you want to generate the function project. For example your project has the name structure `Foo.Bar` as a prefix to all your namespaces. If you want the function project to be generated inside a folder called `Foo.Bar` you'll do the following:

```shell
% cd functions

% dotnet new stacks-az-func-cosmosdb-worker -n Foo.Bar -do Menu
The template "Amido Stacks Azure Function CosmosDb Worker" was created successfully.

% ls -la
total 0
drwxr-xr-x  3 amido  staff   96 23 Aug 15:51 .
drwxr-xr-x  9 amido  staff  288 16 Aug 14:06 ..
drwxr-xr-x  6 amido  staff  192 23 Aug 15:51 Foo.Bar

% ls -la Foo.Bar
total 16
drwxr-xr-x  6 amido  staff   192 23 Aug 15:51 .
drwxr-xr-x  3 amido  staff    96 23 Aug 15:51 ..
-rw-r--r--  1 amido  staff   639 23 Aug 15:51 Dockerfile
drwxr-xr-x  9 amido  staff   288 23 Aug 15:51 Foo.Bar.Worker
drwxr-xr-x  4 amido  staff   128 23 Aug 15:51 Foo.Bar.Worker.UnitTests
-rw-r--r--  1 amido  staff  1643 23 Aug 15:51 Foo.Bar.Worker.sln
```

As you can see your `Foo.Bar` namespace prefix got added to the class names and is reflected not only in the filename, but inside the codebase as well.

To generate the template with your own namespace, but in a different folder you'll have to pass the `-o` flag with your desired path.

```shell
% dotnet new stacks-az-func-cosmosdb-worker -n Foo.Bar -o cosmosdb-worker
The template "Amido Stacks Azure Function CosmosDb Worker" was created successfully.

% ls -la
total 0
drwxr-xr-x  3 amido  staff   96 23 Aug 15:58 .
drwxr-xr-x  9 amido  staff  288 16 Aug 14:06 ..
drwxr-xr-x  6 amido  staff  192 23 Aug 15:58 cosmosdb-worker

% ls -la cosmosdb-worker
total 16
drwxr-xr-x  6 amido  staff   192 23 Aug 15:58 .
drwxr-xr-x  3 amido  staff    96 23 Aug 15:58 ..
-rw-r--r--  1 amido  staff   639 23 Aug 15:58 Dockerfile
drwxr-xr-x  9 amido  staff   288 23 Aug 15:58 Foo.Bar.Worker
drwxr-xr-x  4 amido  staff   128 23 Aug 15:58 Foo.Bar.Worker.UnitTests
-rw-r--r--  1 amido  staff  1643 23 Aug 15:58 Foo.Bar.Worker.sln
```

Now you can build the solution and run/deploy it. If you want to add the existing projects to your own solution you can go to the folder where your `.sln` file lives and execute the following commands

```shell
% cd my-proj-folder

% ls -la
total 16
drwxr-xr-x  6 amido  staff   192 23 Aug 15:58 .
drwxr-xr-x  3 amido  staff    96 23 Aug 15:58 ..
-rw-r--r--  1 amido  staff   639 23 Aug 15:58 src
-rw-r--r--  1 amido  staff  1643 23 Aug 15:58 Foo.Bar.sln

% dotnet sln add path/to/function/Foo.Bar.Worker
% dotnet sln add path/to/function/Foo.Bar.Worker.UnitTests
```

Now both `Foo.Bar.Worker` and `Foo.Bar.Worker.UnitTests` projects are part of your `Foo.Bar` solution.

### Running the API locally on MacOS

To run the API locally on MacOS there are a couple of prerequisites that you have to be aware of. You'll need a CosmosDB emulator and access to Azure Service Bus.

#### Docker CosmosDB emulator setup

1. Get Docker from here - https://docs.docker.com/docker-for-mac/install/
2. Follow the instructions outlined here - https://docs.microsoft.com/en-us/azure/cosmos-db/linux-emulator?tabs=ssl-netstd21
3. From the CosmosDB UI create a database called `Stacks`.
4. Inside the `Stacks` database create a container called `Menu`

#### Azure Service Bus

You'll need an Azure Service Bus namespace and a topic with subscriber in order to be able to publish application events.

#### Azure Event Hub

You'll will need an Azure Event Hub namespace and an Event Hub to publish application events. You will also need a blob container storage account.

#### Configuring CosmosD, ServiceBus or EventHub

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
},
"EventHubConfiguration": {
    "Publisher": {
        "NamespaceConnectionString": {
            "Identifier": "EVENTHUB_CONNECTIONSTRING",
            "Source": "Environment"
        },
        "EventHubName": "stacks-event-hub"
    },
    "Consumer": {
        "NamespaceConnectionString": {
            "Identifier": "EVENTHUB_CONNECTIONSTRING",
            "Source": "Environment"
        },
        "EventHubName": "stacks-event-hub",
        "BlobStorageConnectionString": {
            "Identifier": "STORAGE_CONNECTIONSTRING",
            "Source": "Environment"
        },
        "BlobContainerName": "stacks-blob-container-name"
    }
}
```

The `SecurityKeySecret` and `ConnectionStringSecret` sections are needed because of our use of the `Amido.Stacks.Configuration` package. `COSMOSDB_KEY`, `SERVICEBUS_CONNECTIONSTRING` or `EVENTHUB_CONNECTIONSTRING` have to be set before you can run the project. If you want to debug the solution with VSCode you usually have a `launch.json` file. In that file there's an `env` section where you can put environment variables for the current session.

```json
"env": {
    "ASPNETCORE_ENVIRONMENT": "Development",
    "COSMOSDB_KEY": "YOUR_COSMOSDB_PRIMARY_KEY",
    "SERVICEBUS_CONNECTIONSTRING": "YOUR_SERVICE_BUS_CONNECTION_STRING",
    "EVENTHUB_CONNECTIONSTRING": "YOUR_EVENT_HUB_CONNECTION_STRING"
}
```

If you want to run the application without VSCode you'll have to set the `COSMOSDB_KEY`, `SERVICEBUS_CONNECTIONSTRING` or `EVENTHUB_CONNECTIONSTRING` environment variables through your terminal.

```shell
export COSMOSDB_KEY=YOUR_COSMOSDB_PRIMARY_KEY
export SERVICEBUS_CONNECTIONSTRING=YOUR_SERVICE_BUS_CONNECTION_STRING
export EVENTHUB_CONNECTIONSTRING=YOUR_EVENT_HUB_CONNECTION_STRING
```

This will set the environment variables only for the current session of your terminal.

To set the environment variables permanently on your system you'll have to edit your `bash_profile` or `.zshenv` depending on which shell are you using.

```shell
# Example for setting env variable in .zchenv
echo 'export COSMOSDB_KEY=YOUR_COSMOSDB_PRIMARY_KEY' >> ~/.zshenv
echo 'export SERVICEBUS_CONNECTIONSTRING=YOUR_SERVICE_BUS_CONNECTION_STRING' >> ~/.zshenv
echo 'export EVENTHUB_CONNECTIONSTRING=YOUR_EVENT_HUB_CONNECTION_STRING' >> ~/.zshenv
```

If you wan to run the application using Visual Studio, you will need to set the environment variables in the `launchSettings.json` file contained in the Properties folder of the solution.

```json
    "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "COSMOSDB_KEY": "",
        "SERVICEBUS_CONNECTIONSTRING": "",
        "EVENTHUB_CONNECTIONSTRING": "=",
        "STORAGE_CONNECTIONSTRING": "",
        "OTLP_SERVICENAME": "",
        "OTLP_ENDPOINT": ""
    },
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
