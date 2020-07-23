namespace WebGateway.Services.Endpoints
{
    public static class AuthAPIService
    {
        private static string authAPI = "http://localhost:5001/";

        public static string Endpoint
        {
            get { return authAPI; }
        }
    }
}
