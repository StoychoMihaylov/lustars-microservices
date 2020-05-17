namespace ProfileAPI.Services.Interfaces
{
    using System;
    using ProfileAPI.Data.Entities;
    using ProfileAPI.Models.BidingModels;

    public interface IProfileService
    {
        bool EditUserProfile(UserProfileBindingModel userProfile);
        bool CreateNewUserProfile(CreateUserProfileBindingModel createUserProfileBm);
        UserProfile GetUserProfileById(Guid userId);
        bool CreateNewUserProfileImage(Guid userId, string imageUrl);
        bool SaveUserProfileAvatarImage(Guid userIdGuid, string url);
        bool UpdateUserProfileGeolocation(Guid userIdGuid, GeoLocation geolocation);
    }
}
