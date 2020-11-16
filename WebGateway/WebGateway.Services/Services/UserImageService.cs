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

    public class UserImageService : Service, IUserImageService
    {
        public UserImageService(HttpClient httpClient, StringContentSerializer stringContentSerializer)
            : base(httpClient, stringContentSerializer) { }

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
    }
}
