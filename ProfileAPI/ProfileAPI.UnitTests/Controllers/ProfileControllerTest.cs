namespace ProfileAPI.UnitTests.Controllers
{
    using Moq;
    using Xunit;
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ProfileAPI.Data.Entities;
    using ProfileAPI.App.Controllers;
    using ProfileAPI.Services.Interfaces;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Models.ViewModels;

    public class ProfileControllerTest
    {
        private CreateUserProfileBindingModel createUserProfileBindingModel = new CreateUserProfileBindingModel()
        {
            Id = new Guid("e9166940-f14b-491c-99ba-cfc6cf13f662"),
            Name = "TestName",
            Email = "test@test.com"
        };

        //private Mock<IProfileService> MockCreateNewUserProfileService()
        //{
        //    var profileSericeMock = new Mock<IProfileService>();
        //    profileSericeMock
        //        .Setup(p => p.CreateNewUserProfile(createUserProfileBindingModel))
        //        .Returns(true);

        //    return profileSericeMock;
        //}

        //[Fact]
        //public void Post_CreateNewUserProfileByAccountId_ShouldReturnStatusCode201Created()
        //{
        //    // Arrangev
        //    var profileSericeMock = MockCreateNewUserProfileService();

        //    var profileController = new ProfileController(profileSericeMock.Object);

        //    // Act
        //    var response = profileController.CreateUserProfile(createUserProfileBindingModel);

        //    // Assert
        //    Assert.NotNull(response);
        //    var result = response as StatusCodeResult;
        //    Assert.Equal(201, result.StatusCode);
        //}

        [Fact]
        public void Post_EditUserProfile_ShouldReturnStatusCode200Ok()
        {
            // Arrange
            EditUserProfileBindingModel bm = new EditUserProfileBindingModel()
            {
                Name = "Goshko",

                Gender = "man",
                DateOfBirth = DateTime.UtcNow
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

            var userProfile = new UserProfileDetailedDataViewModel()
            {
                Name = "Goshko",
                Gender = "man",
                DateOfBirth = DateTime.UtcNow,
            };

            var profileService = new Mock<IProfileService>();
            profileService
                .Setup(p => p.GetCurrentUserProfileDetails(new Guid(userId)))
                .Returns(userProfile);

            var profileController = new ProfileController(profileService.Object);

            // Act
            var response = profileController.GetCurrentUserProfile(userId);

            // Assert
            Assert.NotNull(response);
            var result = response as ObjectResult;
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<UserProfile>(result.Value);
        }
    }
}
