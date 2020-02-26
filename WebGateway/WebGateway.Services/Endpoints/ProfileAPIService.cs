namespace WebGateway.Services.Endpoints
{
    using System.Diagnostics;

    public class ProfileAPIService
    {
        private static string authAPIinDebug = "http://localhost:5002/";

        private static string authAPIinRelease = "http://ProfileAPI:80/";

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
