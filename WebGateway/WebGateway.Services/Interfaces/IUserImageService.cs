namespace WebGateway.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using WebGateway.Models.BidingModels.UserProfile;

    public interface IUserImageService
    {
        Task<string> CallImageAPI_UploadImage(Guid userId, IFormFile formData);
        Task<bool> CallProfileAPI_CreateNewUserProfileImage(Guid userId, ImageUrlBindingModel url);
        Task<string> CallProfileAPI_SaveAvatarImageURL(Guid userId, ImageUrlBindingModel url);
        Task<string> CallProfileAPI_GetCurrentUserAvatarImage(Guid guidId);
        Task<bool> CallProfileaPI_DeleteImage(Guid userId, DeleteUserProfileImageBindingModel image);
    }
}
