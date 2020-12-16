namespace WebGateway.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using WebGateway.Models.DTOs;
    using Microsoft.AspNetCore.Mvc;
    using WebGateway.Services.Identity;
    using WebGateway.App.Authorization;
    using WebGateway.Services.Interfaces;
    using WebGateway.Messaging.Interfaces;
    using WebGateway.Models.BidingModels.UserProfile;

    [ApiController]
    [Route("user-profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly IProfileBusService profileBusService;

        public ProfileController(IProfileService profileService, IProfileBusService profileBusService)
        {
            this.profileService = profileService;
            this.profileBusService = profileBusService;
        }

        [HttpGet]
        [Authorize]
        [Route("visitors")]
        public async Task<IActionResult> GetAllProfileVisitors()
        {
            var currentUserId = IdentityManager.CurrentUserId;

            var visitorsVm = await this.profileService.CallProfileAPI_GetAllProfileVisitors(currentUserId);
            if (visitorsVm == null)
            {
                return StatusCode(404); // Not found;
            }

            return StatusCode(200, visitorsVm);
        }

        [HttpGet]
        [Authorize]
        [Route("likes")]
        public async Task<IActionResult> GetProfilesWhoLikedMe()
        {
            var currentUserId = IdentityManager.CurrentUserId;

            var whoLikedMe = await this.profileService.CallProfileAPI_GetWhoLikedMe(currentUserId);
            if (whoLikedMe == null)
            {
                return StatusCode(404); // Not Found!
            }

            return StatusCode(200, whoLikedMe);
        }

        [HttpPost]
        [Authorize]
        [Route("like")]
        public async Task<IActionResult> LikeUserProfile(string id)
        {
            var userId = new Guid();
            var isIdValid = Guid.TryParse(id, out userId);
            if (!isIdValid)
            {
                return StatusCode(400); // Bad Request
            }

            var like = new UserProfileLikeDTO()
            {
                LikeFrom = IdentityManager.CurrentUserId,
                LikeTo = userId
            };

            var response = await this.profileService.CallProfileAPI_LikeUserProfileById(like);
            if (!response)
            {
                return StatusCode(501); // Not Implemented! 
            }

            return StatusCode(200); // OK
        }

        [HttpPost]
        [Authorize]
        [Route("geolocation/update")]
        public async Task<IActionResult> UpdateGeolocation([FromBody] GeoLocationBindingModel bm)
        {
            var userId = IdentityManager.CurrentUserId;

            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model state is not valid!");
            }

            var isUpdated = await this.profileService.CallProfileAPI_UpdateUserProfileGeoLocation(userId, bm);
            if (!isUpdated)
            {
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(200); // Ok!
        }

        [HttpPost]
        [Authorize]
        [Route("edit")]
        public async Task<IActionResult> EditUserProfile([FromBody] UserProfileBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model state is not valid!");
            }

            this.profileBusService.MessageProfileAPI_UpdateUserProfile(bm);

            return StatusCode(202); // Accepted!
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var userId = new Guid();
            var currentUserId = IdentityManager.CurrentUserId;
 
            var isValid = Guid.TryParse(id, out userId);
            if (!isValid)
            {
                return StatusCode(400); // Bad Request!
            }
            
            var userProfileJSON = await this.profileService.CallProfileAPI_GetUserProfileById(currentUserId, userId);
            if (userProfileJSON == null)
            {
                return StatusCode(404); // Not Found!
            }

            return StatusCode(200, userProfileJSON);
        }

        [HttpGet]
        [Authorize]
        [Route("current")]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var userId = IdentityManager.CurrentUserId;
            
            var userProfileJSON = await this.profileService.CallProfileAPI_GetCurrentUserProfile(userId);
            if (userProfileJSON == null)
            {
                return StatusCode(404); // Not Found!
            }

            return StatusCode(200, userProfileJSON);
        }

        [HttpGet]
        [Authorize]
        [Route("short-preview-data")]
        public async Task<IActionResult> GetUserProfileShortPreviewData()
        {
            var userId = IdentityManager.CurrentUserId;

            var userProfileJSON = await this.profileService.CallProfileAPI_GetUserProfileShortreviewDataById(userId);
            if (userProfileJSON == null)
            {
                return StatusCode(404); // Not Found!
            }

            return StatusCode(200, userProfileJSON);
        }

        [HttpGet]
        [Authorize]
        [Route("people-nearby")]
        public async Task<IActionResult> GetPeopleNearby()
        {
            var userId = IdentityManager.CurrentUserId;

            var allUsersInDistance = await this.profileService.GetAllUserInDistance(userId);
            if (allUsersInDistance == null)
            {
                return StatusCode(404); // Not Found!
            }

            return StatusCode(200, allUsersInDistance);
        }
    }
}
