namespace WebGateway.Services.Endpoints
{
    using System.Diagnostics;

    public class ImageAPIService
    {
        private static string imageAPIinDebug = "http://localhost:5003/";

        private static string imageAPIinRelease = "http://ImageAPI:80/";

        public static string Endpoint = GetEndpoint();

        private static string GetEndpoint()
        {
            if (Debugger.IsAttached)
            {
                return imageAPIinDebug;
            }
            else
            {
                return imageAPIinRelease;
            }
        }
    }
}
