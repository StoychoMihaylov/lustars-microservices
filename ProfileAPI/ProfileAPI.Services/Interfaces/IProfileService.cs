﻿namespace ProfileAPI.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using ProfileAPI.Data.Entities;
    using System.Collections.Generic;
    using ProfileAPI.Models.ViewModels;
    using ProfileAPI.Models.BidingModels;

    public interface IProfileService
    {
        Task<bool> EditUserProfile(EditUserProfileBindingModel userProfile);
        Task<bool> CreateNewUserProfile(UserProfile newProfile);
        UserProfileDetailedDataViewModel GetUserProfileDetailsById(Guid currentUserId, Guid userId);
        UserProfileDetailedDataViewModel GetCurrentUserProfileDetails(Guid userId);
        bool UpdateUserProfileGeolocation(Guid userIdGuid, GeoLocation geolocation);
        List<UserProfileVisitorViewModel> GetAllProfileVisitors(Guid id);
        UserProfileShortPreviewDataViewModel GetUserProfileShortPreviewDataById(Guid guidOutput);
        List<UserWhoLikedMeViewModel> GetUsersWhoLikedMe(Guid id);
        List<UserProfileInDistanceViewModel> GetAllUsersInDistance(Guid guidId, int v);
        bool AddUserProfileLike(UserProfileLikeBindingModel like);
        bool CheckIfUsersLikeEachOther(ChatConversationBindingModel bm);
        Guid CreateChatConversation(ChatConversationBindingModel bm);
        Guid? ChechIfConversationBetweenThoseUsersAlreadyExist(ChatConversationBindingModel bm);
    }
}
