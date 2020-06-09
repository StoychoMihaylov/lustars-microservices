namespace ProfileAPI.App.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ProfileAPI.Data.Entities;
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
        [Route("create")]
        public IActionResult CreateUserProfile(CreateUserProfileBindingModel bm)
        {
            var isCreated = this.profileService.CreateNewUserProfile(bm);
            if (!isCreated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(201); // Created!
        }

        [HttpPost]
        [Route("my-user-profile/edit")]
        public IActionResult EditUserProfile([FromBody] EditUserProfileBindingModel bm)
        {
            var isUpdated = this.profileService.EditUserProfile(bm);
            if (!isUpdated)
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

        [HttpGet]
        [Route("my-user-profile-short-preview/{userId}")]
        public IActionResult GetUserProfileShortPreview(string userId)
        {
            var guidOutput = Guid.Empty;
            bool isValid = Guid.TryParse(userId, out guidOutput);
            if (!isValid)
            {
                return StatusCode(400, "The user id is not a in valid Guid format!");
            }

            var userProfileVm = this.profileService.GetUserProfileShortPreviewDataById(guidOutput);
            if (userProfileVm == null)
            {
                return StatusCode(404); // NotFound!
            }

            return StatusCode(200, userProfileVm);
        }

        [HttpPost]
        [Route("{userId}/image-url")]
        public IActionResult CreateNewUserProfileImage(string userId, [FromBody] AddImageUrlBindingModel imageUrl)
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

        [HttpPost]
        [Route("{userId}/image/delete")]
        public IActionResult DeleteUserProfileImage(string userId, [FromBody] DeleteUserProfileImageBindingModel image)
        {
            var userGuidId = Guid.Empty;
            Guid.TryParse(userId, out userGuidId);
           
            var isDeleted = this.profileService.DeleteUserProfileImage(userGuidId, image.Id);

            if (!isDeleted)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(200); // Ok
        }

        [HttpPost]
        [Route("{userId}/avatar-image-url")]
        public IActionResult SaveAvatarImageUrl(string userId, [FromBody]AddImageUrlBindingModel imageUrl)
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

            var isImageCreated = this.profileService.SaveUserProfileAvatarImage(userIdGuid, imageUrl.Url);
            if (!isImageCreated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(201); // Created!
        }

        [HttpPost]
        [Route("{userId}/geolocation")]
        public IActionResult UpdateGeolocation(string userId, [FromBody]GeoLocation geolocation)
        {
            var userIdGuid = Guid.Empty;
            bool isValid = Guid.TryParse(userId, out userIdGuid);
            if (!isValid)
            {
                return StatusCode(400, "The user id is not in a valid Guid format!");
            }

            var updated = this.profileService.UpdateUserProfileGeolocation(userIdGuid, geolocation);
            if (!updated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(200); // Ok
        }
    }
}
