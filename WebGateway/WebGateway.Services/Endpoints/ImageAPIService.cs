namespace WebGateway.Services.Endpoints
{
    public static class ImageAPIService
    {
        private static string imageAPI = "http://ImageAPI:80/";

        public static string Endpoint
        {
            get { return imageAPI; }
        }
    }
}
