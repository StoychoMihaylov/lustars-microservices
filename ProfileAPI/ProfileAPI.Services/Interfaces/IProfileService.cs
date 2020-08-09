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
        UserProfileDetailedDataViewModel GetUserProfileDetailsById(Guid currentUserId, Guid userId);
        UserProfileDetailedDataViewModel GetCurrentUserProfileDetails(Guid userId);
        bool UpdateUserProfileGeolocation(Guid userIdGuid, GeoLocation geolocation);
        List<UserProfileVisitorViewModel> GetAllProfileVisitors(Guid id);
        UserProfileShortPreviewDataViewModel GetUserProfileShortPreviewDataById(Guid guidOutput);
        List<UserWhoLikedMeViewModel> GetUsersWhoLikedMe(Guid id);
        List<UserProfileInDistanceViewModel> GetAllUsersInDistance(Guid guidId, int v);
        bool AddUserProfileLike(UserProfileLikeBindingModel like);
    }
}
