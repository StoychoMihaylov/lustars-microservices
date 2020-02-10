namespace WebGateway.Services.Endpoints
{
    using System.Diagnostics;

    public static class AuthAPIService
    {
        private static string authAPIinDebug = "http://localhost:5001/";

        private static string authAPIinRelease = "http://AuthAPI:80/";

        public static string Endpoint = GetEndpoint();

        private static string GetEndpoint()
        {
            if (Debugger.IsAttached)
            {
                return authAPIinDebug;
            }
            else
            {
                return authAPIinRelease;
            }
        }
    }
}
