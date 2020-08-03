namespace ProfileAPI.UnitTests.Services
{
    using Xunit;
    using System;
    using System.Linq;
    using ProfileAPI.Data.Entities;
    using ProfileAPI.Services.Services;

    public class ImageServiceTest : TestsInitializer
    {
        [Fact]
        public void CreateNewUserProfileImage_ShouldReturnTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var imageUrl = "images/mimi_sexy/e2166920-f54b-131c-88ba-cdc6cd13d662.jpg";
            var user = new UserProfile() { Id = userId };

            var db = this.GetDatabase();
            db.UserProfiles.Add(user);
            db.SaveChanges();

            var profileService = new ImageService(db);

            // Act
            var response = profileService.CreateNewUserProfileImage(userId, imageUrl);
            var createdImg = db.Images
                .Where(i => i.UserProfile.Id == userId)
                .FirstOrDefault();

            // Assert
            Assert.True(response);
            Assert.NotNull(createdImg);
            Assert.Equal(imageUrl, createdImg.Url);
        }
    }
}
