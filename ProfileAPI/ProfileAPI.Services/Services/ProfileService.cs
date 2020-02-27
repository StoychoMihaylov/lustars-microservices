namespace ProfileAPI.Services.Services
{
    using System;

    using ProfileAPI.Data.Entities;
    using ProfileAPI.Data.Interfaces;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Services.Interfaces;

    public class ProfileService : Service, IProfileService
    {
        public ProfileService(IProfileDBContext context)
            : base(context) {}

        public bool EditUserProfile(UserProfileBindingModel bm)
        {
            try
            {
                var userProfile = this.Context
                    .UserProfiles
                    .Find(bm.Id);

                userProfile.Name = bm.Name;
                userProfile.Email = bm.Email;
                userProfile.Gender = bm.Gender;
                userProfile.DateOfBirth = bm.DateOfBirth;
                userProfile.Biography = bm.Biography;
                userProfile.City = bm.City;
                userProfile.LookingFor = bm.LookingFor;
                userProfile.AgeRangeFrom = bm.AgeRangeFrom;
                userProfile.AgeRangeTo = bm.AgeRangeTo;
                userProfile.WantToHaveKids = bm.WantToHaveKids;
                userProfile.EducationDegree = bm.EducationDegree;
                userProfile.University = bm.University;
                userProfile.Work = bm.Work;
                userProfile.Languages = bm.Languages;
                userProfile.EmailNotificationsSubscribe = bm.EmailNotificationsSubscribe;
                userProfile.IsActivated = bm.IsActivated;

                this.Context.UserProfiles.Update(userProfile);
                this.Context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool CreateNewUserProfile(Guid accountId)
        {
            try
            {
                UserProfile newProfile = new UserProfile()
                {
                    Id = accountId,
                    CreatedOn = DateTime.UtcNow
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
