namespace WebGateway.Services.Services
{
    using System;
    using System.Net;
    using System.Text;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    using WebGateway.Services.Endpoints;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.UserProfile;
    using WebGateway.Models.ViewModels.UserProfileViewModel;
    using WebGateway.Models.ViewModels;

    public class ProfileService : Service, IProfileService
    {
        public ProfileService(HttpClient httpClient)
            : base(httpClient) { }

        public async Task<bool> CallProfileAPICreateUserProfile(Guid userId)
        {  
            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/create/{userId.ToString()}", null);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CallProfileAPIEditUserProfile(UserProfileBindingModel bm)
        {
            var stringContent = SerializeObjectToStringContent(bm);
            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + "profile/edit", stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<UserProfileViewModel> CallProfileAPIGetUserProfileById(Guid guidUserId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/{guidUserId.ToString()}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var accountCredentialsVm = JsonConvert
                    .DeserializeObject<UserProfileViewModel>(response.Content.ReadAsStringAsync().Result);

                return accountCredentialsVm;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateNewUserProfileImage(Guid userId, ImageUrlBindingModel url)
        {
            var stringContent = SerializeObjectToStringContent(url);
            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/{userId.ToString()}/image-url", stringContent);
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
