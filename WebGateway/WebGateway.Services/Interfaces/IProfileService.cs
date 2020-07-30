namespace WebGateway.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using WebGateway.Models.BidingModels.UserProfile;

    public interface IProfileService
    {
        Task<bool> CallProfileAPI_CreateUserProfile(CreateUserProfileBindingModel createUserProfileViewModel);
        Task<bool> CallProfileAPI_EditUserProfile(UserProfileBindingModel bm);
        Task<string> CallProfileAPI_GetUserProfileById(Guid guidUserId);
        Task<string> CallProfileAPI_SaveAvatarImageURL(Guid userId, ImageUrlBindingModel url);
        Task<bool> CallProfileAPI_CreateNewUserProfileImage(Guid userId, ImageUrlBindingModel url);
        Task<string> CallImageAPI_UploadImage(Guid userId, IFormFile formData);
        Task<bool> CallProfileAPI_UpdateUserProfileGeoLocation(Guid userId, GeoLocationBindingModel bm);
        Task<bool> CallProfileaPI_DeleteImage(Guid userId, DeleteUserProfileImageBindingModel image);
        Task<string> CallProfileAPI_GetUserProfileShortreviewDataById(Guid userId);
        Task<string> GetAllUserInDistance(Guid guidId);
        Task<string> CallProfileAPI_GetCurrentUserAvatarImage(Guid guidId);
    }
}
