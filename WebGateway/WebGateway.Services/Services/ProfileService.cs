namespace WebGateway.Services.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using WebGateway.Services.Common;
    using WebGateway.Services.Endpoints;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.UserProfile;
    using WebGateway.Models.DTOs;

    public class ProfileService : Service, IProfileService
    {
        public ProfileService(HttpClient httpClient, StringContentSerializer stringContentSerializer)
            : base(httpClient, stringContentSerializer) { }

        public async Task<bool> CallProfileAPI_CreateUserProfile(CreateUserProfileBindingModel bindingModel)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(bindingModel);
            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/create", stringContent);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

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

        public async Task<bool> CallProfileAPI_CreateNewUserProfileImage(Guid userId, ImageUrlBindingModel url)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(url);

            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/{userId.ToString()}/image-url", stringContent);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        public async Task<string> CallProfileAPI_SaveAvatarImageURL(Guid userId, ImageUrlBindingModel url)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(url);

            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/{userId.ToString()}/avatar-image-url", stringContent);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return response.Content.ReadAsStringAsync().Result;
            }

            return null;
        }

        public async Task<string> CallImageAPI_UploadImage(Guid userId, IFormFile formData)
        {
            var ms = new MemoryStream();

            formData
                .OpenReadStream()
                .CopyTo(ms);

            var payload = ms.ToArray();
            var form = new MultipartFormDataContent();
            form.Add(new ByteArrayContent(payload), "image", formData.Name);

            var response = await this.HttpClient.PostAsync(ImageAPIService.Endpoint + $"image/{userId.ToString()}/upload", form);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }

            return null;
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

        public async Task<bool> CallProfileaPI_DeleteImage(Guid userId, DeleteUserProfileImageBindingModel image)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(image);

            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/{userId.ToString()}/image/delete", stringContent);

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

        public async Task<string> CallProfileAPI_GetCurrentUserAvatarImage(Guid guidId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/{guidId.ToString()}/avatar-image-url");

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
    }
}
