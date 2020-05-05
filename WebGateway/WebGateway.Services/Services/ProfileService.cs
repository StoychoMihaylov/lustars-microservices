﻿namespace WebGateway.Services.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    using WebGateway.Services.Common;
    using WebGateway.Models.ViewModels;
    using WebGateway.Services.Endpoints;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.UserProfile;

    public class ProfileService : Service, IProfileService
    {
        public ProfileService(HttpClient httpClient, StringContentSerializer stringContentSerializer)
            : base(httpClient, stringContentSerializer) { }

        public async Task<bool> CallProfileAPI_CreateUserProfile(Guid userId)
        {  
            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/create/{userId.ToString()}", null);

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

        public async Task<UserProfileViewModel> CallProfileAPI_GetUserProfileById(Guid guidUserId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/my-user-profile/{guidUserId.ToString()}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userProfileVm = JsonConvert
                    .DeserializeObject<UserProfileViewModel>(response.Content.ReadAsStringAsync().Result);

                return userProfileVm;
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

        public async Task<bool> CallProfileAPI_SaveAvatarImageURL(Guid userId, ImageUrlBindingModel url)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(url);

            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/{userId.ToString()}/avatar-image-url", stringContent);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
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

        public async Task<bool> CallProfileAPI_UpdateUserProfileGeoLocation(Guid userId, GeoLocation bm)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(bm);

            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/{userId.ToString()}/geolocation", stringContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
