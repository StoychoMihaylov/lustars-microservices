namespace ProfileAPI.UnitTests.Controllers
{
    using Moq;
    using Xunit;
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ProfileAPI.App.Controllers;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Services.Interfaces;

    public class ImageControllerTest
    {
        [Fact]
        public void Post_SaveImageUrl_ShouldReturnStatusCode201()
        {
            // Arrange
            var userId = "e9166940-f14b-491c-99ba-cfc6cf13f662";
            var imageUrl = new AddImageUrlBindingModel() { Url = "images/mimi_sexy/e2166920-f54b-131c-88ba-cdc6cd13d662.jpg" };

            var profileService = new Mock<IImageService>();
            profileService
                .Setup(p => p.SaveUserProfileAvatarImage(new Guid(userId), imageUrl.Url))
                .Returns(true);

            var profileController = new ImageController(profileService.Object);

            // Act
            var response = profileController.SaveAvatarImageUrl(userId, imageUrl);

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
            var imageUrl = new AddImageUrlBindingModel() { Url = "images/mimi_sexy/e2166920-f54b-131c-88ba-cdc6cd13d662.jpg" };

            var profileService = new Mock<IImageService>();
            profileService
                .Setup(p => p.SaveUserProfileAvatarImage(Guid.NewGuid(), imageUrl.Url))
                .Returns(true);

            var profileController = new ImageController(profileService.Object);

            // Act
            var response = profileController.SaveAvatarImageUrl(userId, imageUrl);

            // Assert
            Assert.NotNull(response);
            var result = response as ObjectResult;
            Assert.Equal(400, result.StatusCode);
        }
    }
}
