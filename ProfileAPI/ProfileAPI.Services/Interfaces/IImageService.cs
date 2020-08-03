namespace ProfileAPI.Services.Interfaces
{
    using System;

    public interface IImageService
    {
        bool CreateNewUserProfileImage(Guid userId, string imageUrl);
        bool SaveUserProfileAvatarImage(Guid userIdGuid, string url);
        bool DeleteUserProfileImage(Guid userGuidId, long imageGuidId);
        string GetCurrentUserAvatarImageUrl(Guid guidId);
    }
}
