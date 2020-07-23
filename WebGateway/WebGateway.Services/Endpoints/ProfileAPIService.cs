namespace WebGateway.Services.Endpoints
{
    public static class ProfileAPIService
    {
        private static string profileAPI = "http://localhost:5002/";

        public static string Endpoint
        {
            get { return profileAPI; }
        }
    }
}
