namespace WebGateway.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using WebGateway.Models.ViewModels;
    using WebGateway.Models.BidingModels.UserProfile;
    using WebGateway.Models.ViewModels.UserProfileViewModel;

    public interface IProfileService
    {
        Task<bool> CallProfileAPI_CreateUserProfile(Guid userId);
        Task<bool> CallProfileAPI_EditUserProfile(UserProfileBindingModel bm);
        Task<UserProfileViewModel> CallProfileAPI_GetUserProfileById(Guid guidUserId);
        Task<bool> CallProfileAPI_SaveAvatarImageURL(Guid userId, ImageUrlBindingModel url);
        Task<bool> CallProfileAPI_CreateNewUserProfileImage(Guid userId, ImageUrlBindingModel url);
        Task<string> CallImageAPI_UploadImage(Guid userId, IFormFile formData);
    }
}
