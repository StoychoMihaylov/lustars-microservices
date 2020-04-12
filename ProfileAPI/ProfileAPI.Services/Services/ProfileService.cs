namespace ProfileAPI.Services.Services
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using ProfileAPI.Data.Entities;
    using System.Collections.Generic;
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

                userProfile.EmailNotificationsSubscribed = bm.EmailNotificationsSubscribed;
                userProfile.IsUserProfileActivated = bm.IsUserProfileActivated;
                userProfile.Name = bm.Name;
                userProfile.LastName = bm.Name;
                userProfile.Title = bm.Title;
                userProfile.Gender = bm.Gender;
                userProfile.DateOfBirth = bm.DateOfBirth;
                userProfile.LookingFor = bm.LookingFor;
                userProfile.Biography = bm.Biography;
                userProfile.EducationDegree = bm.EducationDegree;
                userProfile.University = bm.University;
                userProfile.Work = bm.Work;
                userProfile.Languages = bm.Languages;
                userProfile.WantToHaveKids = bm.WantToHaveKids;
                userProfile.Height = bm.Height;
                userProfile.Weight = bm.Weight;
                userProfile.Figure = bm.Figure;
                userProfile.WantKids = bm.WantKids;
                userProfile.HaveKids = bm.HaveKids;
                userProfile.DrinkAlcohol = bm.DrinkAlcohol;
                userProfile.HowOftenDrinkAlcohol = bm.HowOftenDrinkAlcohol;
                userProfile.Smoker = bm.Smoker;
                userProfile.HowOftenSmoke = bm.HowOftenSmoke;
                userProfile.Income = bm.Income;
                userProfile.MeritalStatus = bm.MeritalStatus;
                userProfile.PartnerAgeRangeFrom = bm.PartnerAgeRangeFrom;
                userProfile.PartnerAgeRangeTo = bm.PartnerAgeRangeTo;
                userProfile.PartnerIncomeFrom = bm.PartnerIncomeFrom;
                userProfile.PartnerIncomeTo = bm.PartnerIncomeTo;
                userProfile.PartnerSmoke = bm.PartnerSmoke;
                userProfile.PartnerDrinkAlcohol = bm.PartnerDrinkAlcohol;
                userProfile.PartnerHaveKids = bm.PartnerHaveKids;
                userProfile.PartnerFigure = bm.PartnerFigure;

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
            catch (Exception ex)
            {
                // LOG Exeption
                return false;
            }

            return true;
        }

        public UserProfile GetUserProfileById(Guid userId)
        {
            var userProfile = this.Context
                .UserProfiles
                .AsNoTracking()
                .Where(u => u.Id == userId)
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

            userProfile.Images = userImages;
            userProfile.GeoLocations = geoLocation;

            return userProfile;
        }

        public bool CreateNewUserProfileImage(Guid userId, string imageUrl)
        {
            try
            {
                var img = new List<Image>()
                {
                    new Image()
                    {
                        Url = imageUrl,
                        UploadedOn = DateTime.UtcNow
                    }
                };

                var user = this.Context
                    .UserProfiles
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();

                user.Images = img;

                this.Context.UserProfiles.Update(user);
                this.Context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool SaveUserProfileAvatarImage(Guid userId, string imageUrl)
        {
            try
            {
                var user = this.Context
                .UserProfiles
                .Where(u => u.Id == userId)
                .First();

                user.AvatarImage = imageUrl;

                this.Context.UserProfiles.Update(user);
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
