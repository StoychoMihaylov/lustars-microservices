namespace ProfileAPI.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Services.Interfaces;
    using System;

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
        public IActionResult CreateUserProfile([FromBody] UserProfileBindingModel userProfile)
        {
            if (userProfile == null)
            {
                return StatusCode(400, "Id can't be empty!"); // BadRequest!
            }

            var isCreated = this.profileService.CreateNewUserProfile(userProfile.Id);
            if (!isCreated)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(201); // Created!
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult EditUserProfile(UserProfileBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model state is not valid!");
            }

            var isCraeted = this.profileService.EditUserProfile(bm);
            if (!isCraeted)
            {
                return StatusCode(501); // NotImplemented!
            }

            return StatusCode(200); // Ok!
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserProfile(string userId)
        {
            var guidOutput = Guid.Empty;
            bool isValid = Guid.TryParse(userId, out guidOutput);
            if (!isValid)
            {
                return StatusCode(400, "the id is not in valid Guid format!");
            }

            var userProfileVm = this.profileService.GetUserProfileById(guidOutput);
            if (userProfileVm == null)
            {
                return StatusCode(404); // NotFound!
            }

            return StatusCode(200, userProfileVm);
        }
    }
}
