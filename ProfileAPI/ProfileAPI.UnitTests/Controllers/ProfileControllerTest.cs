﻿namespace ProfileAPI.UnitTests.Controllers
{
    using Moq;
    using Xunit;
    using System;
    using Microsoft.AspNetCore.Mvc;

    using ProfileAPI.App.Controllers;
    using ProfileAPI.Services.Interfaces;
    using ProfileAPI.Models.BidingModels;

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
            var response = profileController.CreateUserProfile(new UserProfileBindingModel()
            { 
                Id = new Guid("e9166940-f14b-491c-99ba-cfc6cf13f662")
            });

            // Assert
            Assert.NotNull(response);
            var result = response as StatusCodeResult;
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void Post_CreateNewUserProfileByNullInput_ShouldReturnStatusCode400BadRequest()
        {
            // Arrange
            var profileSericeMock = MockCreateNewUserProfileService();

            var profileController = new ProfileController(profileSericeMock.Object);

            // Act
            var response = profileController.CreateUserProfile(null);

            // Assert
            Assert.NotNull(response);
            var result = response as ObjectResult;
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Id can't be empty!", result.Value);
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
    }
}
