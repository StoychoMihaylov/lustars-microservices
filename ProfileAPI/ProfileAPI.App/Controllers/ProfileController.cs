namespace ProfileAPI.App.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
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
        [Route("{id}")]
        public IActionResult CreateUserProfile([FromBody] Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                return StatusCode(400, "Id can't be empty!"); // BadRequest
            }

            var isCreated = this.profileService.CreateNewUserProfile(accountId);
            if (!isCreated)
            {
                return StatusCode(501); // NotImplemented
            }

            return StatusCode(201); // Created!
        }
    }
}
