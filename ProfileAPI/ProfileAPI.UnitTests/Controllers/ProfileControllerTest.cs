namespace ProfileAPI.UnitTests.Controllers
{
    using Moq;
    using Xunit;
    using System;
    using Microsoft.AspNetCore.Mvc;

    using ProfileAPI.App.Controllers;
    using ProfileAPI.Services.Interfaces;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Models.ViewModels;

    public class ProfileControllerTest
    {
        private Mock<IProfileService> MockCreateNewUserProfileService()
        {
            var accountId = "e9166940-f14b-491c-99ba-cfc6cf13f662";

            var profileSericeMock = new Mock<IProfileService>();
            profileSericeMock
                .Setup(p => p.CreateNewUserProfile(new Guid(accountId)))
                .Returns(true);

            return profileSericeMock;
        }

        [Fact]
        public void Post_CreateNewUserProfileByAccountId_ShouldReturnStatusCode201Created()
        {
            // Arrangev
            var profileSericeMock = MockCreateNewUserProfileService();

            var profileController = new ProfileController(profileSericeMock.Object);

            // Act
            var response = profileController.CreateUserProfile("e9166940-f14b-491c-99ba-cfc6cf13f662");

            // Assert
            Assert.NotNull(response);
            var result = response as StatusCodeResult;
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void Post_EditUserProfile_ShouldReturnStatusCode200Ok()
        {
            // Arrange
            UserProfileBindingModel bm = new UserProfileBindingModel()
            {
                Name = "Goshko",
                Email = "goshko@abv.bg",
                Gender = "man",
                DateOfBirth = DateTime.UtcNow,
                AgeRangeFrom = 18,
                AgeRangeTo = 30
            }; 

            var profileService = new Mock<IProfileService>();
            profileService
                .Setup(p => p.EditUserProfile(bm))
                .Returns(true);

            var profileController = new ProfileController(profileService.Object);

            // Act
            var response = profileController.EditUserProfile(bm);

            // Assert
            Assert.NotNull(response);
            var result = response as StatusCodeResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Get_GetUserProfileById_ShouldReturnCurrentUserAndStatusCode200()
        {
            // Arrange
            var userId = "e9166940-f14b-491c-99ba-cfc6cf13f662";

            var userProfile = new UserProfileViewModel() 
            {
                Name = "Goshko",
                Email = "goshko@abv.bg",
                Gender = "man",
                DateOfBirth = DateTime.UtcNow,
                AgeRangeFrom = 18,
                AgeRangeTo = 30
            };

            var profileService = new Mock<IProfileService>();
            profileService
                .Setup(p => p.GetUserProfileById(new Guid(userId)))
                .Returns(userProfile);

            var profileController = new ProfileController(profileService.Object);

            // Act
            var response = profileController.GetUserProfile(userId);

            // Assert
            Assert.NotNull(response);
            var result = response as ObjectResult;
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<UserProfileViewModel>(result.Value);
        }

        [Fact]
        public void Post_SaveImageUrl_ShouldReturnStatusCode201()
        { 
            // Arrange
            var userId = "e9166940-f14b-491c-99ba-cfc6cf13f662";
            var imageUrl = new ImageUrlBindingModel() { Url = "images/mimi_sexy/e2166920-f54b-131c-88ba-cdc6cd13d662.jpg" };

            var profileService = new Mock<IProfileService>();
            profileService
                .Setup(p => p.CreateNewUserProfileImage(new Guid(userId), imageUrl.Url))
                .Returns(true);

            var profileController = new ProfileController(profileService.Object);

            // Act
            var response = profileController.SaveImageUrl(userId, imageUrl);

            // Assert
            Assert.NotNull(response);
            var result = response as StatusCodeResult;
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void Post_SaveImageUrlWithInvalidGuidFormat_ShouldReturnStatusCode201()
        {
            // Arrange
            var userId = "wrongGuidFormar-f14b-491c-99ba-wrongGuidFormar";
            var imageUrl = new ImageUrlBindingModel() { Url = "images/mimi_sexy/e2166920-f54b-131c-88ba-cdc6cd13d662.jpg" };

            var profileService = new Mock<IProfileService>();
            profileService
                .Setup(p => p.CreateNewUserProfileImage(Guid.NewGuid(), imageUrl.Url))
                .Returns(true);

            var profileController = new ProfileController(profileService.Object);

            // Act
            var response = profileController.SaveImageUrl(userId, imageUrl);

            // Assert
            Assert.NotNull(response);
            var result = response as ObjectResult;
            Assert.Equal(400, result.StatusCode);
        }
    }
}
