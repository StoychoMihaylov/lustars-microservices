namespace WebGateway.App.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.UserProfile;
    using WebGateway.App.Authorization;
    using WebGateway.Models.ViewModels;

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
        [Route("image-url")]
        public async Task<IActionResult> SaveImageUrl([FromBody] ImageUrlBindingModel imageUrl)
        {
            var userId = IdentityManager.CurrentUserId;

            if (imageUrl.Url == string.Empty)
            {
                return StatusCode(400, "Image url can't be empty string");
            }

            var isImageCreated = await this.profileService.CreateNewUserProfileImage(userId, imageUrl);
            if (!isImageCreated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(201); // Created!
        }
    }
}
