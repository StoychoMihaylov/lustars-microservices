namespace ProfileAPI.App.Controllers
{
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
        [Route("create")]
        public IActionResult CreateUserProfile([FromBody] UserProfileBindngModel userProfile)
        {
            if (userProfile == null)
            {
                return StatusCode(400, "Id can't be empty!"); // BadRequest
            }

            var isCreated = this.profileService.CreateNewUserProfile(userProfile.Id);
            if (!isCreated)
            {
                return StatusCode(501); // NotImplemented
            }

            return StatusCode(201); // Created!
        }
    }
}
