namespace ProfileAPI.UnitTests.Controllers
{
    using Moq;
    using Xunit;
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ProfileAPI.App.Controllers;
    using ProfileAPI.Services.Interfaces;
    using FluentAssertions;
    using System.Net;

    public class ProfileControllerTest
    {
        [Fact]
        public void Post_CreateNewUserProfileByAccountId_ShouldReturnStatusCode201Created()
        {
            // Arrange
            var accountId = Guid.NewGuid();

            var profileSericeMock = new Mock<IProfileService>();
            profileSericeMock
                .Setup(p => p.CreateNewUserProfile(accountId))
                .Returns(true);

            var profileController = new ProfileController(profileSericeMock.Object);

            // Act
            var response = profileController.CreateUserProfile(accountId);

            // Assert
            Assert.NotNull(response);
            dynamic statusCode = response;
            Assert.Equal(201, statusCode.StatusCode);
        }
    }
}
