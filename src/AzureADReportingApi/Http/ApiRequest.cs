using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace AzureADReportingApi.Http
{
    internal class ApiRequest : IApiRequest
    {
        public ApiRequest(string uri) : this(uri, HttpMethod.Get)
        {
        }

        public ApiRequest(string uri, HttpMethod httpMethod)
        {
            Uri = uri;
            Method = httpMethod;
            QueryParameters = new List<Tuple<string, string>>();
        }

        public string Uri { get; set; }
        public HttpMethod Method { get; set; }
        public List<Tuple<string, string>> QueryParameters { get; set;}

        public void AddQueryParameter(string key, string value)
        {
            QueryParameters.Add(new Tuple<string, string>(key, value));
        }

        public void AddQueryParameter(string key, IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                QueryParameters.Add(new Tuple<string, string>(key, value));
            }
        }

        public string RequestUrl()
        {
            var url = $"{ApiConstants.GraphResourceBaseUrl}/{Uri}";

            var parameters = QueryParameters.Select(
                p => FormatQueryStringParameter(p.Item1, p.Item2)).ToList();

            parameters.Add(FormatQueryStringParameter("api-version", ApiConstants.Version));
            return url + "?" + string.Join("&", parameters);
        }

        private static string FormatQueryStringParameter(string key, string value)
        {
            return $"{System.Uri.EscapeUriString(key)}={WebUtility.UrlEncode(value)}";
        }
    }
}