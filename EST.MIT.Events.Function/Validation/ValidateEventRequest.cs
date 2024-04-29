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
                        ""type"": ""string""
                        }
                    },
                    ""required"": [
                        ""type"",
                        ""message"",
                        ""timestamp""
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