namespace WebGateway.App.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.UserProfile;
    using WebGateway.App.Authorization;
    using WebGateway.Models.ViewModels;
    using Microsoft.AspNetCore.Http;

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
        [Route("edit")]
        public async Task<IActionResult> EditUserProfile([FromBody] UserProfileBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model state is not valid!");
            }

            var isCraeted = await this.profileService.CallProfileAPIEditUserProfile(bm);
            if (!isCraeted)
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

            var userProfileVm = await this.profileService.CallProfileAPIGetUserProfileById(userId);
            if (userProfileVm == null)
            {
                return StatusCode(404); // NotFound!
            }

            return StatusCode(200, userProfileVm);
        }

        [HttpPost]
        [Authorize]
        [Route("image/upload")]
        public async Task<IActionResult> SaveImageUrl([FromForm]IFormFile formData)
        {
            var userId = IdentityManager.CurrentUserId;

            if (formData == null) { return StatusCode(400); }
            var isImageInValidFormat = CheckIfImageIsInValidFormat(formData);
            if (!isImageInValidFormat) { return StatusCode(400, "The image must be in jpg(jpeg) format!"); }

            var imageUrl = await this.profileService.CallImageAPIUploadImage(userId, formData);

            if (imageUrl == null)
            {
                return StatusCode(501);
            }

            var isImageCreated = await this.profileService
                .CreateNewUserProfileImage(userId, new ImageUrlBindingModel() { Url = imageUrl });

            if (!isImageCreated)
            {
                return StatusCode(501); // NotImplemented!
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
    }
}
