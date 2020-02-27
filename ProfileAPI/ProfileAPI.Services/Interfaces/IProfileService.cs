namespace ProfileAPI.Services.Interfaces
{
    using System;

    using ProfileAPI.Models.BidingModels;

    public interface IProfileService
    {
        bool EditUserProfile(UserProfileBindingModel userProfile);
        bool CreateNewUserProfile(Guid accountId);
    }
}
