namespace ProfileAPI.UnitTests.Services
{
    using Xunit;
    using System;
    using AutoMapper;
    using ProfileAPI.Data.Entities;
    using ProfileAPI.Services.Services;
    using ProfileAPI.Models.BidingModels;

    public class ProfileServiceTest : TestsInitializer
    {
        private readonly IMapper mapper;

        [Fact]
        public void CreateNewUserProfile_ShouldCreateUserProfileAndReturnTrue()
        {
            // Arrange
            var db = this.GetDatabase();

            var bm = new CreateUserProfileBindingModel() 
            { 
                Id = new Guid("e9166940-f14b-491c-99ba-cfc6cf13f662"),
                Name = "TestName",
                Email = "test@test.com"
            };

            var profileService = new ProfileService(db, this.mapper);

            // Act
            var response = profileService.CreateNewUserProfile(bm);

            var createdUserProfile = db.UserProfiles.Find(bm.Id);

            // Assert
            Assert.True(response);
            Assert.NotNull(createdUserProfile);
            Assert.IsType<UserProfile>(createdUserProfile);
            Assert.Equal(bm.Id, createdUserProfile.Id);
            Assert.Equal(bm.Name, createdUserProfile.Name);
            Assert.Equal(bm.Email, createdUserProfile.Email);
        }

        [Fact]
        public void EditUserProfile_ShouldEditExistingUserProfile()
        {
            // Arrange
            var existinUserProfile = new UserProfile() 
            {
                Id = new Guid("e9166940-f14b-491c-99ba-cfc6cf13f662"),
                Name = "Pesho",
                Gender = "man",
                DateOfBirth = DateTime.UtcNow,
            };

            var db = this.GetDatabase();
            db.UserProfiles.Add(existinUserProfile);
            db.SaveChanges();

            var bm = new EditUserProfileBindingModel() 
            {
                Id = new Guid("e9166940-f14b-491c-99ba-cfc6cf13f662"),
                Name = "Goshko",                     // Changed!!!
                Gender = "man",
                DateOfBirth = DateTime.UtcNow,
            };

            var profileService = new ProfileService(db, this.mapper);

            // Act
            var response = profileService.EditUserProfile(bm);

            var updatedUserProfile = db.UserProfiles.Find(bm.Id);

            // Assert
            Assert.True(response);
            Assert.Equal("Goshko", updatedUserProfile.Name);
        }

        [Fact]
        public void GetUserProfileById_ShouldReturnUserProfileVm()
        {
            // Arrange
            var userId = new Guid("e9166940-f14b-491c-99ba-cfc6cf13f662");

            var userProfile = new UserProfile()
            {
                Id = userId,
                Name = "Goshko",
                Gender = "man",
                DateOfBirth = DateTime.UtcNow,
            };

            var db = this.GetDatabase();
            db.Add(userProfile);
            db.SaveChanges();

            var profileService = new ProfileService(db, this.mapper);

            // Act
            var response = profileService.GetUserProfileById(userId);

            // Assert
            Assert.NotNull(response);
        }
    }
}
