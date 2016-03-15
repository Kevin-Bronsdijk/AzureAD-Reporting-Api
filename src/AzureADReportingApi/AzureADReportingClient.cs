using System;
using System.Threading;
using System.Threading.Tasks;
using AzureADReportingApi.Http;
using AzureADReportingApi.Models;

namespace AzureADReportingApi
{
    public class AzureAdReportingClient : IDisposable
    {
        private AzureConnection _connection;

        public AzureAdReportingClient(AzureConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private RequestParameters CreateRequestParameters(string reportName)
        {
            var requestParameters = new RequestParameters
            {
                TenantDomain = _connection.TenantDomain,
                ReportName = reportName
            };

            return requestParameters;
        }

        public Task<IApiResponse<AuditEvents>> GetAuditEvents()
        {
            return GetAuditEvents(default(CancellationToken));
        }

        public Task<IApiResponse<AuditEvents>> GetAuditEvents(CancellationToken cancellationToken)
        {
            var request = new Request {RequestParameters = CreateRequestParameters("auditEvents")};

            var message = _connection.Execute<AuditEvents>(new ApiRequest(request), cancellationToken);

            return message;
        }

        ~AzureAdReportingClient()
        {
            Dispose(false);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
    }
}