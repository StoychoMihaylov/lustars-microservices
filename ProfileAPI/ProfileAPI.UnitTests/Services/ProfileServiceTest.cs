namespace ProfileAPI.UnitTests.Services
{
    using Xunit;
    using System;

    using ProfileAPI.Data.Entities;
    using ProfileAPI.Services.Services;
    using ProfileAPI.Models.BidingModels;

    public class ProfileServiceTest : TestsInitializer
    {
        [Fact]
        public void CreateNewUserProfile_ShouldCreateUserProfileAndReturnTrue()
        {
            // Arrange
            var db = this.GetDatabase();

            var userId = new Guid("e9166940-f14b-491c-99ba-cfc6cf13f662");

            var profileService = new ProfileService(db);

            // Act
            var response = profileService.CreateNewUserProfile(userId);

            var createdUserProfile = db.UserProfiles.Find(userId);

            // Assert
            Assert.True(response);
            Assert.NotNull(createdUserProfile);
            Assert.IsType<UserProfile>(createdUserProfile);
            Assert.Equal(userId, createdUserProfile.Id);
        }

        [Fact]
        public void EditUserProfile_ShouldEditExistingUserProfile()
        {
            // Arrange
            var db = this.GetDatabase();

            var existinUserProfile = new UserProfile() 
            {
                Id = new Guid("e9166940-f14b-491c-99ba-cfc6cf13f662"),
                Name = "Pesho",
                Email = "goshko@abv.bg",
                Gender = "man",
                DateOfBirth = DateTime.UtcNow,
                AgeRangeFrom = 20,
                AgeRangeTo = 25
            };

            db.UserProfiles.Add(existinUserProfile);
            db.SaveChanges();

            var bm = new UserProfileBindingModel() 
            {
                Id = new Guid("e9166940-f14b-491c-99ba-cfc6cf13f662"),
                Name = "Goshko",                     // Changed!!!
                Email = "goshko@abv.bg",
                Gender = "man",
                DateOfBirth = DateTime.UtcNow,
                AgeRangeFrom = 18,                   // Changed!!!
                AgeRangeTo = 30                      // Changed!!!   
            };

            var profileService = new ProfileService(db);

            // Act
            var response = profileService.EditUserProfile(bm);

            var updatedUserProfile = db.UserProfiles.Find(bm.Id);

            // Assert
            Assert.True(response);
            Assert.Equal("Goshko", updatedUserProfile.Name);
            Assert.Equal("goshko@abv.bg", updatedUserProfile.Email);
            Assert.Equal(18, updatedUserProfile.AgeRangeFrom);
            Assert.Equal(30, updatedUserProfile.AgeRangeTo);
        }
    }
}
