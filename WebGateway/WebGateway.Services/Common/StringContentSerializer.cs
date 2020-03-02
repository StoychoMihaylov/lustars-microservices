namespace WebGateway.Services.Common
{
    using System.Text;
    using Newtonsoft.Json;
    using System.Net.Http;

    public class StringContentSerializer
    {
        public StringContent SerializeObjectToStringContent(dynamic bm)
        {
            var dataJSON = JsonConvert.SerializeObject(bm);
            var stringContent = new StringContent(dataJSON, Encoding.UTF8, "application/json");

            return stringContent;
        }
    }
}
