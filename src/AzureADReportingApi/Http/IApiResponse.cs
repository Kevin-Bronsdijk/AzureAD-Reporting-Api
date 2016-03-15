using System.Net;

namespace AzureADReportingApi.Http
{
    public interface IApiResponse<out TResult> : IApiResponse
    {
        TResult Body { get; }
    }

    public interface IApiResponse
    {
        bool Success { get; }
        HttpStatusCode StatusCode { get; }
    }
}