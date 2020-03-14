namespace WebGateway.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using WebGateway.Models.BidingModels.UserProfile;
    using WebGateway.Models.ViewModels.UserProfileViewModel;

    public interface IProfileService
    {
        Task<bool> CallProfileAPICreateUserProfile(Guid userId);
        Task<bool> CallProfileAPIEditUserProfile(UserProfileBindingModel bm);
        Task<UserProfileViewModel> CallProfileAPIGetUserProfileById(Guid guidUserId);
        Task<bool> CreateNewUserProfileImage(Guid userId, string url);
        string CallImageAPIUploadImage(Microsoft.AspNetCore.Http.IFormFile formData);
    }
}
