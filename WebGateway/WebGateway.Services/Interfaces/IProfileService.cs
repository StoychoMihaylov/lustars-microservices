namespace WebGateway.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using WebGateway.Models.DTOs;
    using WebGateway.Models.BidingModels.UserProfile;

    public interface IProfileService
    {
        Task<bool> CallProfileAPI_EditUserProfile(UserProfileBindingModel bm);
        Task<string> CallProfileAPI_GetCurrentUserProfile(Guid guidUserId);
        Task<bool> CallProfileAPI_UpdateUserProfileGeoLocation(Guid userId, GeoLocationBindingModel bm);
        Task<string> CallProfileAPI_GetAllProfileVisitors(Guid userId);
        Task<string> CallProfileAPI_GetWhoLikedMe(Guid currentUserId);
        Task<string> CallProfileAPI_GetUserProfileShortreviewDataById(Guid userId);
        Task<string> GetAllUserInDistance(Guid guidId);
        Task<bool> CallProfileAPI_LikeUserProfileById(UserProfileLikeDTO like);
        Task<string> CallProfileAPI_GetUserProfileById(Guid currentUserId, Guid userId);
        Task<string> CallProfileAPI_CreateConversationIfUsersLikeEachOther(Guid currentUserId, Guid secondUserId);
    }
}
