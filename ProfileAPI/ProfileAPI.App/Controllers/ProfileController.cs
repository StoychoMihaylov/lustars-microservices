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

        [HttpGet]
        [Route("people-nearby/{userId}")]
        public IActionResult GetPeopleNearby(string userId)
        {
            var guidId = Guid.Parse(userId);

            var allUsersInDistanceOf10km = this.profileService.GetAllUsersInDistance(guidId, 10);
            if (allUsersInDistanceOf10km == null)
            {
                return StatusCode(404); // NotFound!
            }

            return StatusCode(200, allUsersInDistanceOf10km);
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
            var guidId = Guid.Parse(userId);


            var userProfileVm = this.profileService.GetUserProfileById(guidId);
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
            var guidId = Guid.Parse(userId);

            var userProfileVm = this.profileService.GetUserProfileShortPreviewDataById(guidId);
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
            var guidId = Guid.Parse(userId);

            if (imageUrl.Url == string.Empty)
            {
                return StatusCode(400, "Image url can't be empty string");
            }

            var isImageCreated = this.profileService.CreateNewUserProfileImage(guidId, imageUrl.Url);
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

            var isDeleted = this.profileService.DeleteUserProfileImage(guidId, image.Id);

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

            var isImageCreated = this.profileService.SaveUserProfileAvatarImage(guidId, image.Url);
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

            var avatarImg = this.profileService.GetCurrentUserAvatarImageUrl(guidId);
            if (avatarImg == null)
            {
                return StatusCode(404); // Not found!!
            }

            return StatusCode(200, avatarImg); // OK
        }

        [HttpPost]
        [Route("{userId}/geolocation")]
        public IActionResult UpdateGeolocation(string userId, [FromBody]GeoLocation geolocation)
        {
            var guidId = Guid.Parse(userId);
   
            var updated = this.profileService.UpdateUserProfileGeolocation(guidId, geolocation);
            if (!updated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(200); // Ok
        }
    }
}
