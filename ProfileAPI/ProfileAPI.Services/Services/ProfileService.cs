namespace ProfileAPI.Services.Services
{
    using System;
    using AutoMapper;
    using System.Linq;
    using ProfileAPI.Data.Entities;
    using System.Collections.Generic;
    using ProfileAPI.Data.Interfaces;
    using ProfileAPI.Models.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Services.Interfaces;

    public class ProfileService : Service, IProfileService
    {
        private readonly IMapper mapper;

        public ProfileService(IProfileDBContext context, IMapper mapper)
            : base(context) 
        {
            this.mapper = mapper;
        }

        public bool EditUserProfile(EditUserProfileBindingModel bm)
        {
            var languages = GetUpdatedLanguages(bm);

            try
            {
                var userProfile = this.Context
                    .UserProfiles
                    .Find(bm.Id);
             
                this.mapper.Map<EditUserProfileBindingModel, UserProfile>(bm, userProfile);
                userProfile.Languages = languages;

                this.Context.UserProfiles.Update(userProfile);
                this.Context.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        private List<Language> GetUpdatedLanguages(EditUserProfileBindingModel bm)
        {
            var languagesToBeDeled = new List<Language>();
            var languagesToBeAdded = new List<Language>();

            var DBlanguages = this.Context
                .Languages
                .Where(l => l.UserProfile.Id == bm.Id)
                .ToList();

            // TO DO: Add logic that checks if the the db languages and bg languages are equaal whethe some language has been changed

            if (DBlanguages.Count() > bm.Languages.Count())
            {
                foreach (var language in DBlanguages)
                {
                    var isLanguageFound = false;
                    foreach (var bmLanguage in bm.Languages)
                    {
                        if (language.Name == bmLanguage.Name)
                        {
                            isLanguageFound = true;
                        }
                    }

                    if (!isLanguageFound)
                    {
                        languagesToBeDeled.Add(language);
                    }
                }

                DBlanguages.RemoveAll(languagesToBeDeled.Contains);
            }
            else if (DBlanguages.Count() < bm.Languages.Count())
            {
                foreach (var bmLanguage in bm.Languages)
                {
                    var isLanguageFound = false;
                    foreach (var language in DBlanguages)
                    {
                        if (language.Name == bmLanguage.Name)
                        {
                            isLanguageFound = true;
                        }
                    }

                    if (!isLanguageFound)
                    {
                        languagesToBeAdded.Add(bmLanguage);
                    }
                }

                DBlanguages.AddRange(languagesToBeAdded);
            }

            return DBlanguages;
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

        public UserProfileDetailedDataViewModel GetUserProfileById(Guid userId)
        {
            var userProfile = this.mapper.Map<UserProfileDetailedDataViewModel>(this.Context
                .UserProfiles
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .FirstOrDefault());

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
                .FirstOrDefault();

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
            userProfile.GeoLocation = geoLocation;
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

        public bool DeleteUserProfileImage(Guid userGuidId, long imageGuidId)
        {
            try
            {
                var user = this.Context
                   .UserProfiles
                   .Include(u => u.Images)
                   .Where(u => u.Id == userGuidId)
                   .First();

                var imageToBeRemoved = user
                    .Images
                    .Where(i => i.Id == imageGuidId)
                    .First();

                user.Images.Remove(imageToBeRemoved);

                this.Context.UserProfiles.Update(user);
                this.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public UserProfileShortPreviewDataViewModel GetUserProfileShortPreviewDataById(Guid userId)
        {
            var userShortData = this.Context
                .UserProfiles
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new UserProfileShortPreviewDataViewModel() 
                { 
                    Id = u.Id,
                    Name = u.Name,
                    LastName = u.LastName,
                    Credits = u.Credits,
                    Superlikes = u.Superlikes,
                    AvatarImage = u.AvatarImage
                })
                .FirstOrDefault(); // Null if not found!

            var geoLocationShortData = this.Context
                .GeoLocations
                .AsNoTracking()
                .Where(g => g.IsActive == true && g.UserProfile.Id == userId)
                .Select(g => new GeoLocationShortPreviewDataViewModel() 
                { 
                    City = g.City,
                    Country = g.Country
                })
                .FirstOrDefault();

            userShortData.GeoLocation = geoLocationShortData;

            return userShortData;
        }

        public List<UserProfileInDistanceViewModel> GetAllUsersInDistance(Guid guidId, int distance)
        {
            var currentUserGeoLocation = this.Context
                .GeoLocations
                .AsNoTracking()
                .Where(g => g.IsActive == true && g.UserProfile.Id == guidId)
                .FirstOrDefault();

            if (currentUserGeoLocation == null)
            {
                return null;
            }

            var usersInSameCity = this.Context
                .GeoLocations
                .AsNoTracking()
                .Where(g => g.IsActive == true && g.City == currentUserGeoLocation.City)
                .Select(geolocation => new UserProfileInDistanceViewModel()
                {
                    Id = geolocation.UserProfile.Id,
                    Name = geolocation.UserProfile.Name,
                    AvatarImage = geolocation.UserProfile.AvatarImage,
                    City = geolocation.City,
                    Country = geolocation.Country,
                    GeoLocation = geolocation
                })
                .ToList();

            var usersInDistance = FilterAllUsersByDistance(currentUserGeoLocation, usersInSameCity, distance);

            return usersInDistance;
        }

        private List<UserProfileInDistanceViewModel> FilterAllUsersByDistance(
            GeoLocation currentUserGeoLocation, 
            List<UserProfileInDistanceViewModel> usersInSameCity, 
            int distance)
        {
            var usersInDistance = new List<UserProfileInDistanceViewModel>();

            foreach (var user in usersInSameCity)
            {
                var calcDistance = GetDistance(currentUserGeoLocation.Longitude, currentUserGeoLocation.Latitude, user.GeoLocation.Longitude, user.GeoLocation.Latitude);
                if (calcDistance / 1000 <= distance)
                {
                    user.GeoLocation = null; // against loop reference
                    usersInDistance.Add(user);
                }
            }

            return usersInDistance;
        }

        private double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}
