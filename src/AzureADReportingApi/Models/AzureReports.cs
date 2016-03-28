using System.Collections.Generic;
using Newtonsoft.Json;

namespace AzureADReportingApi.Models
{
    public class AzureReports
    {
        [JsonProperty("value")]
        public List<Report> Reports { get; set; }
    }
}
