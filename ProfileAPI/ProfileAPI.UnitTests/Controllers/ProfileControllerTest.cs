namespace ProfileAPI.UnitTests.Controllers
{
    using Moq;
    using Xunit;
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ProfileAPI.App.Controllers;
    using ProfileAPI.Services.Interfaces;

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
        public void Post_CreateNewUserProfileByWrongStringFormatId_ShouldReturnStatusCode400BadRequest()
        {
            // Arrange
            var profileSericeMock = MockCreateNewUserProfileService();

            var profileController = new ProfileController(profileSericeMock.Object);

            // Act
            var response = profileController.CreateUserProfile("wrongStringGuid-wrongStringGuid-wrongStringGuid");

            // Assert
            Assert.NotNull(response);
            var result = response as ObjectResult;
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("The Id should be string in GUID format!", result.Value);
        }

        [Fact]
        public void Post_CreateNewUserProfileByEmptyStringFormatId_ShouldReturnStatusCode400BadRequest()
        {
            // Arrange
            var profileSericeMock = MockCreateNewUserProfileService();

            var profileController = new ProfileController(profileSericeMock.Object);

            // Act
            var response = profileController.CreateUserProfile("");

            // Assert
            Assert.NotNull(response);
            var result = response as ObjectResult;
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Id can't be empty!", result.Value);
        }
    }
}
