using Newtonsoft.Json;

namespace AzureADReportingApi.Models
{
    public class Report
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("licenserequired")]
        public string LicenseRequired { get; set; }
    }
}
