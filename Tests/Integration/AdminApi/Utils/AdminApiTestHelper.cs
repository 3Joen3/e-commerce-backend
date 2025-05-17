namespace Tests.Integration.AdminApi.Utils;

internal static class AdminApiTestHelper
{
    internal static AdminApiFactory GetFactory()
    {
        return new AdminApiFactory();
    }

    internal static HttpClient GetClient()
    {
        return GetFactory().CreateClient();
    }
}