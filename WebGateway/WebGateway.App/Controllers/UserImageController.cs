namespace WebGateway.App.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using WebGateway.App.Authorization;
    using WebGateway.Services.Identity;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.UserProfile;

    [ApiController]
    [Route("user-profile")]
    public class UserImageController : ControllerBase
    {
        private readonly IUserImageService userImageService;

        public UserImageController(IUserImageService userImageService)
        {
            this.userImageService = userImageService;
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

            var imageUrl = await this.userImageService.CallImageAPI_UploadImage(userId, image);

            if (imageUrl == null)
            {
                return StatusCode(501); // Not Implemented!
            }

            var isImageCreated = await this.userImageService
                .CallProfileAPI_CreateNewUserProfileImage(userId, new ImageUrlBindingModel() { Url = imageUrl });

            if (!isImageCreated)
            {
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(201); // Created!
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

        [HttpPost]
        [Authorize]
        [Route("avatar-image/upload")]
        public async Task<IActionResult> avatarImageUpload([FromForm] IFormFile image)
        {
            var userId = IdentityManager.CurrentUserId;

            if (image == null) { return StatusCode(400); }
            var isImageInValidFormat = CheckIfImageIsInValidFormat(image);
            if (!isImageInValidFormat) { return StatusCode(400, "The image must be in 'jpeg' format!"); }

            var imageUrl = await this.userImageService.CallImageAPI_UploadImage(userId, image);

            if (imageUrl == null)
            {
                return StatusCode(501);
            }

            var avatarImgUrl = await this.userImageService.CallProfileAPI_SaveAvatarImageURL(userId,
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

            var avatarURL = await this.userImageService.CallProfileAPI_GetCurrentUserAvatarImage(userId);
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

            var isImageDeleted = await this.userImageService.CallProfileaPI_DeleteImage(userId, image);

            if (!isImageDeleted)
            {
                return StatusCode(501); // Not Implemented!
            }

            return StatusCode(200); // OK!
        }
    }
}
