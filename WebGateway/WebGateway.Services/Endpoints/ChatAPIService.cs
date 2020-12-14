namespace WebGateway.Services.Endpoints
{
    using System.Diagnostics;

    public static class ChatAPIService
    {
        private static string chatAPIinDebug = "http://localhost:5005/";

        private static string chatAPIinRelease = "http://ChatAPI:80/";

        public static string Endpoint = GetEndpoint();

        private static string GetEndpoint()
        {
            if (Debugger.IsAttached)
            {
                return chatAPIinDebug;
            }
            else
            {
                return chatAPIinRelease;
            }
        }
    }
}
