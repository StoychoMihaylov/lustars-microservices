namespace WebGateway.Services.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using WebGateway.Models.DTOs;
    using System.Threading.Tasks;
    using WebGateway.Services.Common;
    using WebGateway.Services.Endpoints;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Chat;
    using WebGateway.Models.BidingModels.UserProfile;

    public class ProfileService : Service, IProfileService
    {
        public ProfileService(HttpClient httpClient, StringContentSerializer stringContentSerializer)
            : base(httpClient, stringContentSerializer) { }

        public async Task<bool> CallProfileAPI_EditUserProfile(UserProfileBindingModel bm)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(bm);
            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + "profile/my-user-profile/edit", stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<string> CallProfileAPI_GetUserProfileById(Guid currentUserId, Guid userId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/user-profile?currentUserId={currentUserId}&userId={userId}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CallProfileAPI_GetCurrentUserProfile(Guid guidUserId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/my-user-profile/{guidUserId.ToString()}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CallProfileAPI_GetUserProfileShortreviewDataById(Guid guidUserId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/my-user-profile-short-preview/{guidUserId.ToString()}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CallProfileAPI_UpdateUserProfileGeoLocation(Guid userId, GeoLocationBindingModel bm)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(bm);

            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/{userId.ToString()}/geolocation", stringContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<string> GetAllUserInDistance(Guid guidId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/people-nearby/{guidId.ToString()}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CallProfileAPI_LikeUserProfileById(UserProfileLikeDTO like)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(like);

            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/like", stringContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> CallProfileAPI_GetWhoLikedMe(Guid currentUserId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/{currentUserId}/likes");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CallProfileAPI_GetAllProfileVisitors(Guid userId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/{userId}/visitors");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }
    }
}
