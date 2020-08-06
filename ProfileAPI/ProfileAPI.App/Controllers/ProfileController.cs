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
        [Route("{id}/likes")]
        public IActionResult GetWhyLikedMe(string id)
        {
            var guidId = Guid.Parse(id);

            var whoLikedMe = this.profileService.GetUsersWhoLikedMe(guidId);
            if (whoLikedMe.Count == 0)
            {
                return StatusCode(404); // Not Found!
            }

            return StatusCode(200, whoLikedMe); // OK!
        }

        [HttpPost]
        [Route("like")]
        public IActionResult LikeUserProfile([FromBody] UserProfileLikeBindingModel like)
        {
            var isAdded = this.profileService.AddUserProfileLike(like);
            if (!isAdded)
            {
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(200); // OK!
        }

        [HttpGet]
        [Route("people-nearby/{userId}")]
        public IActionResult GetPeopleNearby(string userId)
        {
            var guidId = Guid.Parse(userId);

            var allUsersInDistanceOf10km = this.profileService.GetAllUsersInDistance(guidId, 10);
            if (allUsersInDistanceOf10km == null)
            {
                return StatusCode(404); // Not Found!
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
                return StatusCode(501); // Not Implemented!
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
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(200); // Ok!
        }

        [HttpGet]
        [Route("my-user-profile/{userId}")]
        public IActionResult GetCurrentUserProfile(string userId)
        {
            var guidId = Guid.Parse(userId);


            var userProfileVm = this.profileService.GetCurrentUserProfileDetails(guidId);
            if (userProfileVm == null)
            {
                return StatusCode(404); // Not Found!
            }

            return StatusCode(200, userProfileVm);
        }

        [HttpGet]
        [Route("user-profile")]
        public IActionResult GetUserProfile(Guid currentUserId, Guid userId)
        {
            var userProfileVm = this.profileService.GetUserProfileDetailsById(currentUserId, userId);
            if (userProfileVm == null)
            {
                return StatusCode(404); // Not Found!
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
                return StatusCode(404); // Not Found!
            }

            return StatusCode(200, userProfileVm);
        }

        [HttpPost]
        [Route("{userId}/geolocation")]
        public IActionResult UpdateGeolocation(string userId, [FromBody]GeoLocation geolocation)
        {
            var guidId = Guid.Parse(userId);
   
            var updated = this.profileService.UpdateUserProfileGeolocation(guidId, geolocation);
            if (!updated)
            {
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(200); // Ok
        }
    }
}
