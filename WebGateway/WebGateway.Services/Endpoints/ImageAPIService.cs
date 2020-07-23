namespace WebGateway.Services.Endpoints
{
    public static class ImageAPIService
    {
        private static string imageAPI = "http://localhost:5003/";

        public static string Endpoint
        {
            get { return imageAPI; }
        }
    }
}
