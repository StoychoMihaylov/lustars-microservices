namespace ProfileAPI.Services.Interfaces
{
    using System;
    using ProfileAPI.Data.Entities;
    using ProfileAPI.Models.BidingModels;

    public interface IProfileService
    {
        bool EditUserProfile(UserProfileBindingModel userProfile);
        bool CreateNewUserProfile(Guid accountId);
        UserProfile GetUserProfileById(Guid userId);
        bool CreateNewUserProfileImage(Guid userId, string imageUrl);
    }
}
