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
        [Route("create")]
        public IActionResult CreateUserProfile(string stringId)
        {
            if (stringId == string.Empty)
            {
                return StatusCode(400, "Id can't be empty!"); // BadRequest
            }

            var accountId = Guid.Empty;
            var isParsed = Guid.TryParse(stringId, out accountId);
            if (!isParsed)
            {
                return StatusCode(400, "The Id should be string in GUID format!");
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
