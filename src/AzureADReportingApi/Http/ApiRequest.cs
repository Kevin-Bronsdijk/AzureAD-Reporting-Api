using System.Net.Http;
using AzureADReportingApi.Models;

namespace AzureADReportingApi.Http
{
    internal class ApiRequest : IApiRequest
    {
        private const string GraphResourceId = "https://graph.windows.net";

        public ApiRequest(IRequest body) : this(body, HttpMethod.Get)
        {
        }

        public ApiRequest(IRequest body, HttpMethod httpMethod)
        {
            Method = httpMethod;
            Body = body;
            Uri = GetAuditEventsUrl(Body.RequestParameters.TenantDomain,
                Body.RequestParameters.ReportName);
        }

        public string Uri { get; set; }
        public HttpMethod Method { get; set; }
        public IRequest Body { get; set; }

        public static string GetAuditEventsUrl(string tenantDomain, string reportName)
        {
            var url = $"{GraphResourceId}/{tenantDomain}/reports/{reportName}?api-version=beta";

            return url;
        }
    }
}