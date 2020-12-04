namespace ProfileAPI.Services.Services
{
    using System;
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
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

        public async Task<bool> EditUserProfile(EditUserProfileBindingModel bm)
        {
            var languages = await GetUpdatedLanguages(bm);

            try
            {
                var userProfile = await this.Context
                    .UserProfiles
                    .FindAsync(bm.Id);
             
                this.mapper.Map<EditUserProfileBindingModel, UserProfile>(bm, userProfile);
                userProfile.Languages = languages;

                this.Context.UserProfiles.Update(userProfile);
                await this.Context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        private async Task<List<Language>> GetUpdatedLanguages(EditUserProfileBindingModel bm)
        {
            var languagesToBeDeled = new List<Language>();
            var languagesToBeAdded = new List<Language>();

            var DBlanguages = await this.Context
                .Languages
                .Where(l => l.UserProfile.Id == bm.Id)
                .ToListAsync();

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

        public async Task<bool> CreateNewUserProfile(UserProfile newProfile)
        {
            try
            {
                await this.Context.UserProfiles.AddAsync(newProfile);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // LOG Exeption
                return false;
            }

            return true;
        }

        public UserProfileDetailedDataViewModel GetCurrentUserProfileDetails(Guid userId)
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

            var likes = this.Context
                .Likes
                .AsNoTracking()
                .Where(l => l.LikeToId == userId)
                .Count();

            userProfile.Likes = likes;
            userProfile.Images = userImages;
            userProfile.GeoLocation = geoLocation;
            userProfile.Languages = languages;

            return userProfile;
        }

        public UserProfileDetailedDataViewModel GetUserProfileDetailsById(Guid currentUserId, Guid userId)
        {
            var userProfile = MarkUserProfileAsVisited(currentUserId, userId);
            if (userProfile == null) return null;

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

            var likes = this.Context
                .Likes
                .AsNoTracking()
                .Where(l => l.LikeToId == userId)
                .ToListAsync()
                .Result;

            var isAlreadyLikedByCurrentUser = likes
                .Where(l => l.LikeFromId == currentUserId)
                .FirstOrDefault();

            userProfile.disableLikeButton = isAlreadyLikedByCurrentUser != null ? true : false;
            userProfile.Likes = likes.Count;
            userProfile.Images = userImages;
            userProfile.GeoLocation = geoLocation;
            userProfile.Languages = languages;

            return userProfile;
        }

        private UserProfileDetailedDataViewModel MarkUserProfileAsVisited(Guid currentUserId, Guid userId)
        {
            var userProfile = this.Context
                .UserProfiles
                .Include(u => u.ProfileVisits)
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (userProfile == null) { return null; }

            var newVisit = new ProfileVisitor()
            {
                VisitorId = currentUserId,
                VisitedId = userId,
                onDate = DateTime.UtcNow
            };

            var visit = userProfile.ProfileVisits.Where(v => v.VisitorId == currentUserId).FirstOrDefault();
            if (visit == null)
            {
                userProfile.ProfileVisits.Add(newVisit);
            }
            else
            {
                visit.onDate = DateTime.UtcNow;
                this.Context.ProfileVisitor.Update(visit);
            }

            this.Context.SaveChanges();

            return this.mapper.Map<UserProfileDetailedDataViewModel>(userProfile);
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
                    LustarLikes = u.LustarLikes,
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
                .Where(g => g.IsActive == true && g.City == currentUserGeoLocation.City && g.UserProfile.Id != guidId)
                .Select(geolocation => new UserProfileInDistanceViewModel()
                {
                    Id = geolocation.UserProfile.Id,
                    Name = geolocation.UserProfile.Name,
                    AvatarImage = geolocation.UserProfile.AvatarImage,
                    CountImages = geolocation.UserProfile.Images.Count(),
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
                    user.Distance = Math.Round(calcDistance / 1000, 1).ToString() + "km";
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

        public bool AddUserProfileLike(UserProfileLikeBindingModel like)
        {
            var userToLike = this.Context
                .UserProfiles
                //.Include(u => u.Likes)
                .Where(u => u.Id == like.LikeTo)
                .FirstOrDefault();

            if (userToLike == null) return false;

            var newLike = new Like()
            {
                LikeFromId = like.LikeFrom,
                LikeToId = like.LikeTo,
                onDate = DateTime.UtcNow
            };

            var currentUser = this.Context
                .UserProfiles
                //.Include(u => u.WhoILiked)
                .Where(u => u.Id == like.LikeFrom)
                .FirstOrDefault();

            currentUser.WhoILiked.Add(newLike);
            userToLike.Likes.Add(newLike);

            this.Context.UserProfiles.UpdateRange(userToLike, currentUser);
            this.Context.SaveChanges();

            return true;
        }

        public List<UserWhoLikedMeViewModel> GetUsersWhoLikedMe(Guid id)
        {
            var whoLikedMe = this.Context
                .Likes
                .Include(l => l.LikeFrom)
                    .ThenInclude(l => l.GeoLocations)
                .Where(l => l.LikeToId == id)
                .Select(like => new UserWhoLikedMeViewModel()
                {
                    Id = like.LikeFrom.Id,
                    Name = like.LikeFrom.Name,
                    AvatarImage = like.LikeFrom.AvatarImage,
                    CountImages = like.LikeFrom.Images.Count(),
                    GeoLocation = like.LikeFrom.GeoLocations.Where(g => g.IsActive == true).FirstOrDefault()
                })
                .ToList();

            if (whoLikedMe.Count == 0) return whoLikedMe;

            var currentUserGeoLocation = this
                .Context
                .GeoLocations
                .Where(g => g.IsActive == true && g.UserProfile.Id == id)
                .FirstOrDefault();

            foreach (var user in whoLikedMe)
            {
                var calcDistance = GetDistance(
                    user.GeoLocation.Longitude,
                    user.GeoLocation.Latitude,
                    currentUserGeoLocation.Longitude,
                    currentUserGeoLocation.Latitude
                    );
 
                user.Distance = Math.Round(calcDistance / 1000, 1).ToString() + "km";
                user.City = user.GeoLocation.City;
                user.Country = user.GeoLocation.Country;
                user.GeoLocation = null;
            }

            return whoLikedMe;
        }

        public List<UserProfileVisitorViewModel> GetAllProfileVisitors(Guid id)
        {
            var visitors = this.Context
                .ProfileVisitor
                .AsNoTracking()
                .Include(v => v.Visitor)
                    .ThenInclude(v => v.GeoLocations)
                .Where(v => v.VisitedId == id)
                .Select(visit => new UserProfileVisitorViewModel()
                {
                    Id = visit.Visitor.Id,
                    Name = visit.Visitor.Name,
                    AvatarImage = visit.Visitor.AvatarImage,
                    CountImages = visit.Visitor.Images.Count(),
                    GeoLocation = visit.Visitor.GeoLocations.Where(g => g.IsActive == true).FirstOrDefault()
                })
                .ToList();

            if (visitors.Count == 0) return visitors;

            var currentUserGeoLocation = this
               .Context
               .GeoLocations
               .Where(g => g.IsActive == true && g.UserProfile.Id == id)
               .FirstOrDefault();

            foreach (var user in visitors)
            {
                var calcDistance = GetDistance(
                   user.GeoLocation.Longitude,
                   user.GeoLocation.Latitude,
                   currentUserGeoLocation.Longitude,
                   currentUserGeoLocation.Latitude
                   );

                user.Distance = Math.Round(calcDistance / 1000, 1).ToString() + "km";
                user.City = user.GeoLocation.City;
                user.Country = user.GeoLocation.Country;
                user.GeoLocation = null;
            }

            return visitors;
        }

        public bool CheckIfUsersLikeEachOther(ChatConversationBindingModel bm)
        {
            var doTheyLikeEachOther = this.Context
                .Likes
                .Where(like => 
                    (like.LikeFromId == bm.CurrentUserID && like.LikeToId == bm.UserToStartConversationWithID) && 
                    (like.LikeFromId == bm.UserToStartConversationWithID && like.LikeToId == bm.CurrentUserID)
                ).FirstOrDefault();

            if (doTheyLikeEachOther != null)
            {
                return true;
            }

            return false;
        }

        public void CreateChatConversation(ChatConversationBindingModel bm)
        {
            var invitedUser = this.Context
                .UserProfiles
                //.Include(u => u.ChatIvitations)
                .Where(u => u.Id == bm.UserToStartConversationWithID)
                .FirstOrDefault();

            if (invitedUser == null) 
                throw new Exception($"Chat invitaion faild because user with id:{bm.UserToStartConversationWithID} can't be found!");

            var chatConversation = new ChatConversation()
            {
                ChatStarterUserId = bm.CurrentUserID,
                InvitedUserId = bm.UserToStartConversationWithID,
                StartedOn = DateTime.UtcNow
            };

            var currentUser = this.Context
                .UserProfiles
                //.Include(u => u.StartedChatConversations)
                .Where(u => u.Id == bm.CurrentUserID)
                .FirstOrDefault();

            currentUser.StartedChatConversations.Add(chatConversation);
            invitedUser.ChatIvitations.Add(chatConversation);

            this.Context.UserProfiles.UpdateRange(invitedUser, currentUser);
            this.Context.SaveChanges();
        }
    }
}
