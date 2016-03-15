using Newtonsoft.Json;

namespace AzureADReportingApi.Models
{
    public class ErrorResult
    {
        [JsonProperty("message")]
        public string Error { get; set; }
    }
}