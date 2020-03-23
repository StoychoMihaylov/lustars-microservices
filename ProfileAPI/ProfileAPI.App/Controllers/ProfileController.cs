namespace ProfileAPI.App.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Services.Interfaces;

    [ApiController]
    [Route("profile")]
    public class ProfileController : ControllerBase
    {
        private IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpPost]
        [Route("create/{userId}")]
        public IActionResult CreateUserProfile(string userId)
        {
            var guidUserId = Guid.Empty;
            bool isValid = Guid.TryParse(userId, out guidUserId);
            if (!isValid)
            {
                return StatusCode(400, "The user id is not a in valid Guid format!");
            }

            var isCreated = this.profileService.CreateNewUserProfile(guidUserId);
            if (!isCreated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(201); // Created!
        }

        [HttpPost]
        [Route("my-user-profile/edit")]
        public IActionResult EditUserProfile([FromBody] UserProfileBindingModel bm)
        {
            var isCraeted = this.profileService.EditUserProfile(bm);
            if (!isCraeted)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(200); // Ok!
        }

        [HttpGet]
        [Route("my-user-profile/{userId}")]
        public IActionResult GetMyUserProfile(string userId)
        {
            var guidOutput = Guid.Empty;
            bool isValid = Guid.TryParse(userId, out guidOutput);
            if (!isValid)
            {
                return StatusCode(400, "The user id is not a in valid Guid format!");
            }

            var userProfileVm = this.profileService.GetUserProfileById(guidOutput);
            if (userProfileVm == null)
            {
                return StatusCode(404); // NotFound!
            }

            return StatusCode(200, userProfileVm);
        }

        [HttpPost]
        [Route("{userId}/image-url")]
        public IActionResult SaveImageUrl(string userId, [FromBody] ImageUrlBindingModel imageUrl)
        {
            var userIdGuid = Guid.Empty;
            bool isValid = Guid.TryParse(userId, out userIdGuid);
            if (!isValid)
            {
                return StatusCode(400, "The user id is not in a valid Guid format!");
            }

            if (imageUrl.Url == string.Empty)
            {
                return StatusCode(400, "Image url can't be empty string");
            }

            var isImageCreated = this.profileService.CreateNewUserProfileImage(userIdGuid, imageUrl.Url);
            if (!isImageCreated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(201); // Created!
        }
    }
}
