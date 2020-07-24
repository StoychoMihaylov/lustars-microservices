namespace WebGateway.Services.Endpoints
{
    public static class ProfileAPIService
    {
        private static string profileAPI = "http://ProfileAPI:80/";

        public static string Endpoint
        {
            get { return profileAPI; }
        }
    }
}
