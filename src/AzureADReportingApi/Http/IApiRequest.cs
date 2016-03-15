using System.Net.Http;
using AzureADReportingApi.Models;

namespace AzureADReportingApi.Http
{
    internal interface IApiRequest
    {
        string Uri { get; set; }
        IRequest Body { get; set; }
        HttpMethod Method { get; set; }
    }
}