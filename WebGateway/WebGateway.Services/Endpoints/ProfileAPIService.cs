namespace WebGateway.Services.Endpoints
{
    using System.Diagnostics;

    public static class ProfileAPIService
    {
        private static string profileAPIinDebug = "http://localhost:5002/";

        private static string profileAPIinRelease = "http://ProfileAPI:80/";

        public static string Endpoint = GetEndpoint();

        private static string GetEndpoint()
        {
            if (Debugger.IsAttached)
            {
                return profileAPIinDebug;
            }
            else
            {
                return profileAPIinRelease;
            }
        }
    }
}
