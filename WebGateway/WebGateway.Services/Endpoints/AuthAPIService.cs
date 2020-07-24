namespace WebGateway.Services.Endpoints
{
    public static class AuthAPIService
    {
        private static string authAPI = "http://AuthAPI:80/";

        public static string Endpoint
        {
            get { return authAPI; }
        }
    }
}
