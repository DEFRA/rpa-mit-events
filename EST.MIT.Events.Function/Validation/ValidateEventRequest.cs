using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Events.Function.Validation;

public static class ValidateEventRequest
{
    const string schemaJson = @"{
            ""$schema"": ""http://json-schema.org/draft-04/schema#"",
            ""type"": ""object"",
            ""properties"": {
                ""name"": {
                ""type"": ""string""
                },
                ""properties"": {
                ""type"": ""object"",
                ""properties"": {
                    ""id"": {
                    ""type"": ""string""
                    },
                    ""checkpoint"": {
                    ""type"": ""string""
                    },
                    ""status"": {
                    ""type"": ""string""
                    },
                    ""action"": {
                    ""type"": ""object"",
                    ""properties"": {
                        ""type"": {
                        ""type"": ""string""
                        },
                        ""message"": {
                        ""type"": ""string""
                        },
                        ""timestamp"": {
                        ""type"": ""string""
                        },
                        ""data"": {
                        ""type"": ""object"",
                        ""properties"": {
                            ""invoiceId"": {
                            ""type"": ""string""
                            },
                            ""notificationType"": {
                            ""type"": ""string""
                            },
                            ""emailAddress"": {
                            ""type"": ""string""
                            },
                            ""requestBy"": {
                            ""type"": ""string""
                            }
                        },
                        ""required"": [
                            ""invoiceId"",
                            ""notificationType"",
                            ""emailAddress"",
                            ""requestBy""
                        ]
                        }
                    },
                    ""required"": [
                        ""type"",
                        ""message"",
                        ""timestamp"",
                        ""data""
                    ]
                    }
                },
                ""required"": [
                    ""id"",
                    ""checkpoint"",
                    ""status"",
                    ""action""
                ]
                }
            },
            ""required"": [
                ""name"",
                ""properties""
            ]
        }";
    public static bool IsValid(string importRequest)
    {
        var schema = JSchema.Parse(schemaJson);
        var parseImportRequest = JObject.Parse(importRequest);
        return parseImportRequest.IsValid(schema);
    }
}