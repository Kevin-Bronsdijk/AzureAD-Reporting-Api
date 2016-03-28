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

        private ApiRequest GetApiRequest(string reportName)
        {
            return new ApiRequest(string.Format($"{_connection.TenantDomain}/reports/{reportName}"));
        }

        public Task<IApiResponse<AuditEvents>> GetAuditEvents()
        {
            return GetAuditEvents(default(CancellationToken));
        }

        public Task<IApiResponse<AuditEvents>> GetAuditEvents(CancellationToken cancellationToken)
        {
            var request = GetApiRequest("auditEvents");

            var message = _connection.Execute<AuditEvents>(
                request,
                cancellationToken
                );

            return message;
        }

        public Task<IApiResponse<AuditEvents>> GetAuditEvents(DateTime from, DateTime till)
        {
            return GetAuditEvents(from, till, default(CancellationToken));
        }

        public Task<IApiResponse<AuditEvents>> GetAuditEvents(DateTime from, DateTime till, CancellationToken cancellationToken)
        {
            var request = GetApiRequest("auditEvents");

            string filter =
                $"eventTime ge {@from.ToString("yyyy-MM-dd")} and eventTime le {till.ToString("yyyy-MM-dd")}";
            
            request.AddQueryParameter("$filter", filter);
            
            var message = _connection.Execute<AuditEvents>(
                request,
                cancellationToken
                );

            return message;
        }

        public Task<IApiResponse<object>> GetAccountProvisioningEvents()
        {
            return GetAccountProvisioningEvents(default(CancellationToken));
        }

        public Task<IApiResponse<object>> GetAccountProvisioningEvents(CancellationToken cancellationToken)
        {
            var request = GetApiRequest("accountProvisioningEvents");

            throw new NotImplementedException();
        }

        public Task<IApiResponse<object>> GetSignInsFromUnknownSourcesEvents()
        {
            return GetSignInsFromUnknownSourcesEvents(default(CancellationToken));
        }

        public Task<IApiResponse<object>> GetSignInsFromUnknownSourcesEvents(CancellationToken cancellationToken)
        {
            var request = GetApiRequest("signInsFromUnknownSourcesEvents");

            throw new NotImplementedException();
        }

        public Task<IApiResponse<object>> GetSignInsFromMultipleGeographiesEvents()
        {
            return GetSignInsFromMultipleGeographiesEvents(default(CancellationToken));
        }

        public Task<IApiResponse<object>> GetSignInsFromMultipleGeographiesEvents(CancellationToken cancellationToken)
        {
            var request = GetApiRequest("signInsFromMultipleGeographiesEvents");

            throw new NotImplementedException();
        }

        public Task<IApiResponse<object>> GetSignInsFromPossiblyInfectedDevicesEvents()
        {
            return GetSignInsFromPossiblyInfectedDevicesEvents(default(CancellationToken));
        }

        public Task<IApiResponse<object>> GetSignInsFromPossiblyInfectedDevicesEvents(CancellationToken cancellationToken)
        {
            var request = GetApiRequest("signInsFromPossiblyInfectedDevicesEvents");

            throw new NotImplementedException();
        }

        public Task<IApiResponse<object>> GetIrregularSignInActivityEvents()
        {
            return GetIrregularSignInActivityEvents(default(CancellationToken));
        }

        public Task<IApiResponse<object>> GetIrregularSignInActivityEvents(CancellationToken cancellationToken)
        {
            var request = GetApiRequest("irregularSignInActivityEvents");

            throw new NotImplementedException();
        }

        public Task<IApiResponse<object>> GetAllUsersWithAnomalousSignInEvents()
        {
            return GetAllUsersWithAnomalousSignInEvents(default(CancellationToken));
        }

        public Task<IApiResponse<object>> GetAllUsersWithAnomalousSignInEvents(CancellationToken cancellationToken)
        {
            var request = GetApiRequest("allUsersWithAnomalousSignInEvents");

            throw new NotImplementedException();
        }

        public Task<IApiResponse<object>> GetSignInsAfterMultipleFailuresEvents()
        {
            return GetSignInsAfterMultipleFailuresEvents(default(CancellationToken));
        }

        public Task<IApiResponse<object>> GetSignInsAfterMultipleFailuresEvents(CancellationToken cancellationToken)
        {
            var request = GetApiRequest("signInsAfterMultipleFailuresEvents");

            throw new NotImplementedException();
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