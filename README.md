# Introduction 
This repository contains the code for consuming events for invoices

# Getting Started

## Azurite

Follow the following guide to setup Azurite:

- [Azurite emulator for local Azure Storage development](https://dev.azure.com/defragovuk/DEFRA-EST/_wiki/wikis/DEFRA-EST/7722/Azurite-emulator-for-local-Azure-Storage-development)

- [Docker](https://dev.azure.com/defragovuk/DEFRA-EST/_wiki/wikis/DEFRA-EST/9601/Azurite-with-Docker)

## Storage

The function app uses Azure Storage for Table and Queue.

The function app requires:

- Queue name: `event`
- Table name: `event`

## local.settings

```
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "QueueConnectionString": "UseDevelopmentStorage=true",
        "TableConnectionString": "UseDevelopmentStorage=true"
    }
}
```

## Queue

### Message Example

```
{
	"name": "Create Invoice",
	"properties": {
		"id": "1234567890",
		"checkpoint": "est.invoice.web",
		"status": "ApprovalRequired",
		"action": {
			"type": "approval",
			"message": "Invoice requires approval",
			"timestamp": "2023-02-14T15:00:00.000Z",
			"data": {
                "invoiceId": "1234567890",
                "notificationType": "approval",
                "emailAddress": "test@test.com",
                "requestBy": "Geoff"
              }
		}
	}
}
```

## HTTP

### Endpoint

`/api/invoice/events/{invoiceId}`

### Response

```
[{"odata.etag":"W/\"datetime'2023-03-22T11%3A55%3A44.4792631Z'\"","PartitionKey":"1234567890","RowKey":"1234567890_20230322115544","Data":"{\"invoiceId\":\"123456789\",\"notificationType\":\"approval\",\"emailAddress\":\"test@test.com\",\"requestBy\":\"Geoff\"}","EventType":"approval","Timestamp":"2023-03-22T11:55:44.4792631+00:00"}]
```

# Build and Test
To run the function:

`cd EST.MIT.InvoiceImporter.Function`

`func start`

## Useful links

- [gov Notify](https://www.notifications.service.gov.uk/using-notify/api-documentation)

- [Use dependency injection in .NET Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection)