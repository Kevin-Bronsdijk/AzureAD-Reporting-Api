using Newtonsoft.Json;

namespace AzureADReportingApi.Models
{
    public class Request : IRequest
    {
        [JsonIgnore]
        public RequestParameters RequestParameters { get; set; }
    }
}