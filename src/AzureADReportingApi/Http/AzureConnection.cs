using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using AzureADReportingApi.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AzureADReportingApi.Http
{
    public class AzureConnection : IDisposable
    {
        private const string GraphResourceId = "https://graph.windows.net";
        private const string Domain = ".onMicrosoft.com";
        private const string AuthenticationContextAuthority = "https://login.microsoftonline.com";
        private const double TokenCredentialsTimeToLiveInMinutes = 3;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private string _accessToken;
        private HttpClient _client;
        private JsonMediaTypeFormatter _formatter;
        private DateTime _tokenCreationDateTime = DateTime.MinValue;

        internal AzureConnection(string clientId, string clientSecret, string tenantDomain, HttpMessageHandler handler)
        {
            _client = new HttpClient(handler);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            ConfigureSerialization();

            _clientId = clientId;
            _clientSecret = clientSecret;
            TenantDomain = tenantDomain;
        }

        internal string TenantDomain { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void ConfigureSerialization()
        {
            _formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter> {new StringEnumConverter {CamelCaseText = true}},
                    NullValueHandling = NullValueHandling.Ignore
                }
            };
        }

        public static AzureConnection Create(string clientId, string clientSecret, string tenantDomain, IWebProxy proxy = null)
        {
            if (string.IsNullOrEmpty(tenantDomain))
                throw new ArgumentException();
            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentException();
            if (string.IsNullOrEmpty(clientSecret))
                throw new ArgumentException();

            var handler = new HttpClientHandler {Proxy = proxy};
            return new AzureConnection(clientId, clientSecret, tenantDomain, handler);
        }

        internal async Task<IApiResponse<TResponse>> Execute<TResponse>(ApiRequest request, CancellationToken cancellationToken)
        {
            using (var requestMessage = BuildRequest(request))
            {
                using (var responseMessage = await _client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false))
                {
                    return await BuildResponse<TResponse>(responseMessage, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        private HttpRequestMessage BuildRequest(IApiRequest request)
        {
            var requestMessage = new HttpRequestMessage(request.Method, request.RequestUrl());
            requestMessage.Headers.Add("Authorization", "Bearer " +  GetAccessToken().Result);
            return requestMessage;
        }

        private async Task<IApiResponse<TResponse>> BuildResponse<TResponse>(HttpResponseMessage message, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<TResponse>
            {
                StatusCode = message.StatusCode,
                Success = message.IsSuccessStatusCode
            };

            if (message.Content != null)
            {
                if (message.IsSuccessStatusCode)
                {
                    // Debugging /  var test = message.Content.ReadAsStringAsync().Result;

                    response.Body = await message.Content.ReadAsAsync<TResponse>(
                        new[] {_formatter}, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    var errorResponse = await message.Content.ReadAsAsync<ErrorResult>(cancellationToken).ConfigureAwait(false);

                    if (errorResponse != null)
                    {
                        response.Error = errorResponse.Error;
                    }
                }
            }

            return response;
        }

        private async Task<string> GetAccessToken()
        {
            if (!IsValidToken())
            {
                var authContext = new AuthenticationContext($"{AuthenticationContextAuthority}/{$"{TenantDomain}{Domain}"}");
                var credential = new ClientCredential(_clientId, _clientSecret);
                var result = await authContext.AcquireTokenAsync(GraphResourceId, credential);

                _accessToken = result.CreateAuthorizationHeader().Substring("Bearer ".Length);
                _tokenCreationDateTime = DateTime.UtcNow;
            }

            return _accessToken;
        }

        private bool IsValidToken()
        {
            return _accessToken != null &&
                   DateTime.UtcNow < _tokenCreationDateTime.AddMinutes(TokenCredentialsTimeToLiveInMinutes);
        }
        ~AzureConnection()
        {
            Dispose(false);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client != null)
                {
                    _client.Dispose();
                    _client = null;
                }
            }
        }
    }
}