namespace WebGateway.App.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using WebGateway.App.Authorization;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.UserProfile;
    using WebGateway.Models.ViewModels;
    using System;
    using System.Linq;

    [ApiController]
    [Route("user-profile")]
    public class ProfileController : ControllerBase
    {
        private IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }


        [HttpPost]
        [Authorize]
        [Route("geolocation/update")]
        public async Task<IActionResult> UpdateGeolocation([FromBody] GeoLocation bm)
        {
            var userId = IdentityManager.CurrentUserId;

            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model state is not valid!");
            }

            var isUpdated = await this.profileService.CallProfileAPI_UpdateUserProfileGeoLocation(userId, bm);
            if (!isUpdated)
            {
                return StatusCode(501); // NotImplemented!
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
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(200); // Ok!
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = IdentityManager.CurrentUserId;

            var userProfileVm = await this.profileService.CallProfileAPI_GetUserProfileById(userId);
            if (userProfileVm == null)
            {
                return StatusCode(404); // NotFound!
            }

            return StatusCode(200, userProfileVm);
        }

        [HttpPost]
        [Authorize]
        [Route("image/upload")]
        public async Task<IActionResult> uploadImage([FromForm] IFormFile image)
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

            var isImageCreated = await this.profileService
                .CallProfileAPI_CreateNewUserProfileImage(userId, new ImageUrlBindingModel() { Url = imageUrl });

            if (!isImageCreated)
            {
                return StatusCode(501); // NotImplemented!
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

            var isImageCreated = await this.profileService.CallProfileAPI_SaveAvatarImageURL(userId, 
                new ImageUrlBindingModel() 
                { 
                    Url = imageUrl
                });

            if (!isImageCreated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(201); // Created!
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
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(200); // OK!
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
