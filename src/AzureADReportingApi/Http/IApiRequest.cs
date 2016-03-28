using System;
using System.Collections.Generic;
using System.Net.Http;

namespace AzureADReportingApi.Http
{
    internal interface IApiRequest
    {
        string Uri { get; set; }

        HttpMethod Method { get; set; }

        List<Tuple<string, string>> QueryParameters { get; set; }

        void AddQueryParameter(string key, string value);

        string RequestUrl();
    }
}