namespace ProfileAPI.Services.Services
{
    using System;
    using System.Linq;
    using ProfileAPI.Data.Entities;
    using System.Collections.Generic;
    using ProfileAPI.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Services.Interfaces;

    public class ProfileService : Service, IProfileService
    {
        public ProfileService(IProfileDBContext context)
            : base(context) {}

        public bool EditUserProfile(EditUserProfileBindingModel bm)
        {
            var languages = this.Context
                .Languages
                .Where(l => l.UserProfile.Id == bm.Id)
                .ToList();

            languages = bm.Languages.ToList();

            // TO DO: Make it works when some language is deleted

            try
            {
                var userProfile = this.Context
                    .UserProfiles
                    .Find(bm.Id);

                userProfile.Languages = languages;
                userProfile.EmailNotificationsSubscribed = bm.EmailNotificationsSubscribed;
                userProfile.IsUserProfileActivated = bm.IsUserProfileActivated;             
                userProfile.Name = bm.Name;
                userProfile.LastName = bm.LastName;
                userProfile.FromCity = bm.FromCity;
                userProfile.FromCountry = bm.FromCountry;
                userProfile.FeelInMood = bm.FeelInMood;
                userProfile.Gender = bm.Gender;
                userProfile.DateOfBirth = bm.DateOfBirth;
                userProfile.LookingFor = bm.LookingFor;
                userProfile.Biography = bm.Biography;
                userProfile.EducationDegree = bm.EducationDegree;
                userProfile.University = bm.University;
                userProfile.Work = bm.Work;
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
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool CreateNewUserProfile(CreateUserProfileBindingModel bm)
        {
            try
            {
                UserProfile newProfile = new UserProfile()
                {
                    Id = bm.Id,
                    Name = bm.Name,
                    Email = bm.Email,
                    Gender = bm.Gender,
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

            var languages = this.Context
                .Languages
                .AsNoTracking()
                .Where(l => l.UserProfile.Id == userId)
                .Select(l => new Language
                {
                    Id = l.Id,
                    Name = l.Name
                })
                .ToList();

            userProfile.Images = userImages;
            userProfile.GeoLocations = geoLocation;
            userProfile.Languages = languages;

            return userProfile;
        }

        public bool CreateNewUserProfileImage(Guid userId, string imageUrl)
        {
            try
            {
                var imgs = new List<Image>()
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

                user.Images = imgs;

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

        public bool UpdateUserProfileGeolocation(Guid userIdGuid, GeoLocation geolocation)
        {
            try
            {
                var user = this.Context
                   .UserProfiles
                   .Include(u => u.GeoLocations)
                   .Where(u => u.Id == userIdGuid)
                   .First();


                var newGeolocation = new GeoLocation()
                {
                    Street = geolocation.Street,
                    City = geolocation.City,
                    Country = geolocation.Country,
                    Latitude = geolocation.Latitude,
                    Longitude = geolocation.Longitude,
                    CreatedOn = DateTime.UtcNow,
                    IsActive = true,
                    UserProfile = user
                };
                

                if (user.GeoLocations.Count != 0)
                {
                    foreach (var geo in user.GeoLocations)
                    {
                        geo.IsActive = false;
                    }
                }

                user.GeoLocations.Add(newGeolocation);
                this.Context.UserProfiles.Update(user);
                this.Context.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
