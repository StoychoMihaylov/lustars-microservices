namespace ProfileAPI.Services.Services
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    using ProfileAPI.Data.Entities;
    using ProfileAPI.Data.Interfaces;
    using ProfileAPI.Models.ViewModels;
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

        public UserProfileViewModel GetUserProfileById(Guid userId)
        {
            var userProfile = this.Context
                .UserProfiles
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(user => new UserProfile
                {
                    Name = user.Name,
                    Email = user.Email,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    CreatedOn = user.CreatedOn,
                    Biography = user.Biography,
                    City = user.City,
                    Credits = user.Credits,
                    Superlikes = user.Superlikes,
                    LookingFor = user.LookingFor,
                    AgeRangeFrom = user.AgeRangeFrom,
                    AgeRangeTo = user.AgeRangeTo,
                    WantToHaveKids = user.WantToHaveKids,
                    EducationDegree = user.EducationDegree,
                    University = user.University,
                    Work = user.Work,
                    Languages = user.Languages,
                    EmailNotificationsSubscribe = user.EmailNotificationsSubscribe,
                    IsActivated = user.IsActivated
                })
                .FirstOrDefault();

            if (userProfile == null) { return null; }

            var userImages = this.Context
                .Images
                .AsNoTracking()
                .Where(img => img.UserProfile.Id == userId)
                .Select(img => new Image
                {
                    Id = img.Id,
                    Url = img.Url
                })
                .ToList();

            var geoLocation = this.Context
                .GeoLocations
                .AsNoTracking()
                .Where(g => g.IsActive == true && g.UserProfile.Id == userId)
                .Select(g => new GeoLocation
                {
                    Id = g.Id,
                    City = g.City,
                    Country = g.Country
                })
                .ToList();

            var userProfileViewModel = new UserProfileViewModel();
            userProfileViewModel.Name = userProfile.Name;
            userProfileViewModel.Email = userProfile.Email;
            userProfileViewModel.Gender = userProfile.Gender;
            userProfileViewModel.DateOfBirth = userProfile.DateOfBirth;
            userProfileViewModel.CreatedOn = userProfile.CreatedOn;
            userProfileViewModel.Biography = userProfile.Biography;
            userProfileViewModel.City = userProfile.City;
            userProfileViewModel.Credits = userProfile.Credits;
            userProfileViewModel.Superlikes = userProfile.Superlikes;
            userProfileViewModel.LookingFor = userProfile.LookingFor;
            userProfileViewModel.AgeRangeFrom = userProfile.AgeRangeFrom;
            userProfileViewModel.AgeRangeTo = userProfile.AgeRangeTo;
            userProfileViewModel.WantToHaveKids = userProfile.WantToHaveKids;
            userProfileViewModel.EducationDegree = userProfile.EducationDegree;
            userProfileViewModel.University = userProfile.University;
            userProfileViewModel.Work = userProfile.Work;
            userProfileViewModel.Languages = userProfile.Languages;
            userProfileViewModel.EmailNotificationsSubscribe = userProfile.EmailNotificationsSubscribe;
            userProfileViewModel.IsActivated = userProfile.IsActivated;
            userProfileViewModel.Images = userImages;
            userProfileViewModel.GeoLocations = geoLocation;

            return userProfileViewModel;
        }
    }
}
