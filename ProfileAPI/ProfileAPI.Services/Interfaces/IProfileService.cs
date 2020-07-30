namespace ProfileAPI.Services.Interfaces
{
    using System;
    using ProfileAPI.Data.Entities;
    using System.Collections.Generic;
    using ProfileAPI.Models.ViewModels;
    using ProfileAPI.Models.BidingModels; 

    public interface IProfileService
    {
        bool EditUserProfile(EditUserProfileBindingModel userProfile);
        bool CreateNewUserProfile(CreateUserProfileBindingModel createUserProfileBm);
        UserProfileDetailedDataViewModel GetUserProfileById(Guid userId);
        bool CreateNewUserProfileImage(Guid userId, string imageUrl);
        bool SaveUserProfileAvatarImage(Guid userIdGuid, string url);
        bool UpdateUserProfileGeolocation(Guid userIdGuid, GeoLocation geolocation);
        bool DeleteUserProfileImage(Guid userGuidId, long imageGuidId);
        UserProfileShortPreviewDataViewModel GetUserProfileShortPreviewDataById(Guid guidOutput);
        List<UserProfileInDistanceViewModel> GetAllUsersInDistance(Guid guidId, int v);
        string GetCurrentUserAvatarImageUrl(Guid guidId);
    }
}
