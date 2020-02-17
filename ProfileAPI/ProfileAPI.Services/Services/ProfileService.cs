namespace ProfileAPI.Services.Services
{
    using System;

    using ProfileAPI.Data.Entities;
    using ProfileAPI.Data.Interfaces;
    using ProfileAPI.Services.Interfaces;


    public class ProfileService : Service, IProfileService
    {
        public ProfileService(IProfileDBContext context)
            : base(context) {}

        public bool CreateNewUserProfile(Guid accountId)
        {
            try
            {
                UserProfile newProfile = new UserProfile()
                {
                    Id = accountId
                };

                this.Context.UserProfiles.Add(newProfile);
                this.Context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
