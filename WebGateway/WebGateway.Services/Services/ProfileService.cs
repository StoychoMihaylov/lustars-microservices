namespace WebGateway.Services.Services
{
    using System.Net;
    using System.Text;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    using WebGateway.Models.DTOs;
    using WebGateway.Services.Endpoints;
    using WebGateway.Services.Interfaces;

    public class ProfileService : Service, IProfileService
    {
        public ProfileService(HttpClient httpClient)
            : base(httpClient) { }

        public async Task<bool> CallProfileAPICreateUserProfile(UserProfile userProfile)
        {
            var stringContent = SerializeObjectToStringContent(userProfile);
            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + "profile/create", stringContent);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        private StringContent SerializeObjectToStringContent(dynamic bm)
        {
            var dataJSON = JsonConvert.SerializeObject(bm);
            var stringContent = new StringContent(dataJSON, Encoding.UTF8, "application/json");

            return stringContent;
        }
    }
}
