using System;

namespace ProfileAPI.Services.Interfaces
{
    public interface IProfileService
    {
        bool CreateNewUserProfile(Guid accountId);
    }
}
