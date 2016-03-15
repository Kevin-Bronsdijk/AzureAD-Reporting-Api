using Newtonsoft.Json;

namespace AzureADReportingApi.Models
{
    public class Value
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("eventTime")]
        public string EventTime { get; set; }

        [JsonProperty("actor")]
        public string Actor { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("actorDetail")]
        public string ActorDetail { get; set; }

        [JsonProperty("targetDetail")]
        public string TargetDetail { get; set; }

        [JsonProperty("updatedProperties")]
        public string UpdatedProperties { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }

        [JsonProperty("roleDetail")]
        public RoleDetail RoleDetail { get; set; }
    }
}