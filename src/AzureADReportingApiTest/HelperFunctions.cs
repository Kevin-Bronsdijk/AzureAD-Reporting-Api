using AzureADReportingApi.Http;

namespace AzureADReportingApiTest
{
    internal static class HelperFunctions
    {
        public static AzureConnection GetAzureConnection()
        {
            return AzureConnection.Create(
                Settings.ClientId,
                Settings.ClientSecret,
                Settings.TenantDomain
                );
        }

        public static AzureConnection GetAzureConnectionFakeId()
        {
            return AzureConnection.Create(
                "a",
                "b",
                "c"
                );
        }
    }
}
