namespace WebGateway.Services.Services
{
    using System.Net.Http;

    public class Service
    {
        private readonly HttpClient httpClient;

        public Service(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        protected HttpClient HttpClient
        {
            get { return this.httpClient; }
        }
    }
}
