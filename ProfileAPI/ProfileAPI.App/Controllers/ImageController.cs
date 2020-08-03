namespace ProfileAPI.App.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Services.Interfaces;

    [ApiController]
    [Route("profile")]
    public class ImageController : ControllerBase
    {
        private IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        [Route("{userId}/image-url")]
        public IActionResult CreateNewUserProfileImage(string userId, [FromBody] AddImageUrlBindingModel imageUrl)
        {
            var guidId = Guid.Parse(userId);

            if (imageUrl.Url == string.Empty)
            {
                return StatusCode(400, "Image url can't be empty string");
            }

            var isImageCreated = this.imageService.CreateNewUserProfileImage(guidId, imageUrl.Url);
            if (!isImageCreated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(201); // Created!
        }

        [HttpPost]
        [Route("{userId}/image/delete")]
        public IActionResult DeleteUserProfileImage(string userId, [FromBody] DeleteUserProfileImageBindingModel image)
        {
            var guidId = Guid.Parse(userId);

            var isDeleted = this.imageService.DeleteUserProfileImage(guidId, image.Id);

            if (!isDeleted)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(200); // Ok
        }

        [HttpPost]
        [Route("{userId}/avatar-image-url")]
        public IActionResult SaveAvatarImageUrl(string userId, [FromBody]AddImageUrlBindingModel image)
        {
            var guidId = Guid.Parse(userId);

            if (image.Url == string.Empty)
            {
                return StatusCode(400, "Image url can't be empty string");
            }

            var isImageCreated = this.imageService.SaveUserProfileAvatarImage(guidId, image.Url);
            if (!isImageCreated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(201, image.Url); // Created!
        }

        [HttpGet]
        [Route("{userId}/avatar-image-url")]
        public IActionResult GetCurrentUserAvatarImageUrl(string userId)
        {
            var guidId = Guid.Parse(userId);

            var avatarImg = this.imageService.GetCurrentUserAvatarImageUrl(guidId);
            if (avatarImg == null)
            {
                return StatusCode(404); // Not found!!
            }

            return StatusCode(200, avatarImg); // OK
        }
    }
}
