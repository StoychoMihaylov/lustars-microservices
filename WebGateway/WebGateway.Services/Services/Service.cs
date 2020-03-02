namespace WebGateway.Services.Services
{
    using System.Net.Http;
    using WebGateway.Services.Common;

    public class Service
    {
        private readonly HttpClient httpClient;
        private readonly StringContentSerializer stringContentSerializer;

        public Service(HttpClient httpClient, StringContentSerializer stringContentSerializer)
        {
            this.httpClient = httpClient;
            this.stringContentSerializer = stringContentSerializer;
        }

        protected HttpClient HttpClient
        {
            get { return this.httpClient; }
        }

        protected StringContentSerializer StringContentSerializer
        {
            get { return this.stringContentSerializer; }
        }
    }
}
