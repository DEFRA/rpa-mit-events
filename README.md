# Events

This repository contains an azure function with Service Bus and a HTTP Triggers, the messages to the service bus are sent via other services, it's use is as a method of logging what actions have taken place on a manual invoice.

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=est-mit-events&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=est-mit-events) [![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=est-mit-events&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=est-mit-events) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=est-mit-events&metric=coverage)](https://sonarcloud.io/summary/new_code?id=est-mit-events) [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=est-mit-events&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=est-mit-events)
## Requirements

Amend as needed for your distribution, this assumes you are using windows with WSL.
- <details>
    <summary> .NET 8 SDK </summary>
    
    #### Basic instructions for installing the .NET 8 SDK on a debian based system.
  
    Amend as needed for your distribution.

    ```bash
    wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    sudo apt-get update && sudo apt-get install -y dotnet-sdk-8.0
    ```
</details>

- <details>
    <summary> Azure Functions Core Tools </summary>
    
    ```bash
    sudo apt-get install azure-functions-core-tools-4
    ```
</details>

- [Docker](https://docs.docker.com/desktop/install/linux-install/)
- Service Bus Queue

---
## Local Setup

To run this service locally complete the following steps.
### Create Local Settings

Create a local.setttings.json file with the following content.

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
    }
}
```

### Set up user secrets

Use the secrets-template to create a "secrets.json" in the same folder location.

Once this is done run the following command to add the projects user secrets

```bash
cat secrets.json | dotnet user-secrets set
```

These values can also be added to the local settings file, but the preferred method is via user secrets.

### Create emulated table storage

You need to create a local emulation of azure table storage, this can be done using [azurite](https://github.com/Azure/Azurite).

In your console run the following commands.

```bash
docker pull mcr.microsoft.com/azure-storage/azurite
```

```bash
docker run --name azurite -p 10000:10000 -p 10001:10001 -p 10002:10002 mcr.microsoft.com/azure-storage/azurite
```

You can view the emulated storage using a tool such as [Azure Storage Explorer](https://github.com/microsoft/AzureStorageExplorer).
### Startup

To start the function locally.

```bash
func start
```

If running multiple function apps locally you might encounter a port conflict error as they all try to run on port 7071. If this happens use a command such as this entering a port that is free.

```bash
func start --port 7072
```

---
## Usage / Endpoints

### Event Creation
>Function Trigger: ServiceBusTrigger
>#### Endpoint
>Uses the Service Bus queue trigger named from the environment variable `%EventQueueName%` 
>#### Action
>Receives event messages, validates them, and processes them into the system. Valid event data is transformed into event entities and stored. Errors and validations are logged appropriately.
>
>Below is an **encoded** example message that can be added to the service bus queue to test functionality. Json messages format must be encoded as base64 to be accepted.
>
>```base64
>ewoJIm5hbWUiOiAiQ3JlYXRlIEludm9pY2UiLAoJInByb3BlcnRpZXMiOiB7CgkJImlkIjogIjEyMzQ1Njc4OTAiLAoJCSJjaGVja3BvaW50IjogImVzdC5pbnZvaWNlLndlYiIsCgkJInN0YXR1cyI6ICJBcHByb3ZhbFJlcXVpcmVkIiwKCQkiYWN0aW9uIjogewoJCQkidHlwZSI6ICJhcHByb3ZhbCIsCgkJCSJtZXNzYWdlIjogIkludm9pY2UgcmVxdWlyZXMgYXBwcm92YWwiLAoJCQkidGltZXN0YW1wIjogIjIwMjMtMDItMTRUMTU6MDA6MDAuMDAwWiIsCgkJCSJkYXRhIjogIlNvbWUgZGF0YSIKCQl9Cgl9Cn0=
>```

### Event Retrieval
>Function Trigger: HttpTrigger
>#### Endpoint
>```http
>GET /invoice/events/{invoiceId}
>```
>#### Action
>Retrieves all events related to a specific invoice based on the provided invoice ID. The events are fetched from a storage service and returned to the requester. If no events are found, a 404 response is returned.