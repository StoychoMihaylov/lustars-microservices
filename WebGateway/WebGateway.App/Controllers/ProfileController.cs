namespace WebGateway.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using WebGateway.Models.DTOs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using WebGateway.Services.Identity;
    using WebGateway.App.Authorization;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.UserProfile;

    [ApiController]
    [Route("user-profile")]
    public class ProfileController : ControllerBase
    {
        private IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        [Route("visitors")]
        public async Task<IActionResult> GetAllProfileVisitors()
        {
            var currentUserId = IdentityManager.CurrentUserId;

            var visitorsVm = await this.profileService.CallProfileAPI_GetAllProfileVisitors();
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

            var isUpdated = await this.profileService.CallProfileAPI_EditUserProfile(bm);
            if (!isUpdated)
            {
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(200); // Ok!
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

        [HttpPost]
        [Authorize]
        [Route("image/upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
        {
            var userId = IdentityManager.CurrentUserId;

            if (image == null) { return StatusCode(400); }
            var isImageInValidFormat = CheckIfImageIsInValidFormat(image);
            if (!isImageInValidFormat) { return StatusCode(400, "The image must be in 'jpeg' format!"); }

            var imageUrl = await this.profileService.CallImageAPI_UploadImage(userId, image);

            if (imageUrl == null)
            {
                return StatusCode(501); // Not Implemented!
            }

            var isImageCreated = await this.profileService
                .CallProfileAPI_CreateNewUserProfileImage(userId, new ImageUrlBindingModel() { Url = imageUrl });

            if (!isImageCreated)
            {
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(201); // Created!
        }

        [HttpPost]
        [Authorize]
        [Route("avatar-image/upload")]
        public async Task<IActionResult> avatarImageUpload([FromForm]IFormFile image)
        {
            var userId = IdentityManager.CurrentUserId;

            if (image == null) { return StatusCode(400); }
            var isImageInValidFormat = CheckIfImageIsInValidFormat(image);
            if (!isImageInValidFormat) { return StatusCode(400, "The image must be in 'jpeg' format!"); }

            var imageUrl = await this.profileService.CallImageAPI_UploadImage(userId, image);

            if (imageUrl == null)
            {
                return StatusCode(501);
            }

            var avatarImgUrl = await this.profileService.CallProfileAPI_SaveAvatarImageURL(userId,
                new ImageUrlBindingModel()
                {
                    Url = imageUrl
                });

            if (avatarImgUrl == null)
            {
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(201, avatarImgUrl); // Created!
        }

        [HttpGet]
        [Authorize]
        [Route("avatar-image")]
        public async Task<IActionResult> getCurrentUserAvatarImage()
        {
            var userId = IdentityManager.CurrentUserId;

            var avatarURL = await this.profileService.CallProfileAPI_GetCurrentUserAvatarImage(userId);
            if (avatarURL != null)
            {
                return StatusCode(200, avatarURL); // OK
            }

            return StatusCode(200); // Not Found!
        }

        [HttpPost]
        [Authorize]
        [Route("image/delete")]
        public async Task<IActionResult> deleteImage([FromBody] DeleteUserProfileImageBindingModel image)
        {
            if (image.Id == 0 || image.Id == long.MinValue)
            {
                return StatusCode(400); // Bad Request!
            }

            var userId = IdentityManager.CurrentUserId;

            var isImageDeleted = await this.profileService.CallProfileaPI_DeleteImage(userId, image);

            if (!isImageDeleted)
            {
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(200); // OK!
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

        private bool CheckIfImageIsInValidFormat(IFormFile image)
        {
            if (image.ContentType == "image/jpg" ||
                image.ContentType == "image/jpeg")
            {
                return true;
            }

            return false;
        }
    }
}
