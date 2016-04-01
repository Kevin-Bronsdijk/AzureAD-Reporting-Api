using System.Collections.Generic;
using Newtonsoft.Json;

namespace AzureADReportingApi.Models
{
    public class AuditEvents
    {
        [JsonProperty("value")]
        public List<AuditEvent> AuditEvent { get; set; }
    }
}