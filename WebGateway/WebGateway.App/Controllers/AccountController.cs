namespace WebGateway.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Account;
    using WebGateway.App.Infrastructure.Authorization;
    using WebGateway.Models.DTOs;

    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IProfileService profileService;

        public AccountController(IAccountService accountService, IProfileService profileService)
        {
            this.accountService = accountService;
            this.profileService = profileService;
        }

        // account/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAndLogin([FromBody] RegisterUserBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState); // BadRequest!
            }

            if (bm.Password != bm.ConfirmPassword)
            {
                return StatusCode(400, "Invalid credentials!"); // BadRequest!
            }

            try
            {
                var accountCredentials = await this.accountService.CallAuthAPIAccountRegister(bm);
                if (accountCredentials == null)
                {
                    return StatusCode(400, "Email already exists or wrong credentials!"); // BadRequest!
                }

                var userProfile = new UserProfile() { Id = accountCredentials.UserId };
                var isCreated = await this.profileService.CallProfileAPICreateUserProfile(userProfile);

                if (!isCreated)
                {
                    this.accountService.CallAuthAPIDeleteAccount(accountCredentials);
                    return StatusCode(503); // ServiceUnavailable!
                }

                return StatusCode(201, accountCredentials); // Created!
            }
            catch (Exception ex)
            {
                // TO DO: log the exeption here

                return StatusCode(503); // ServiceUnavailable!
            }  
        }

        // account/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState); // BadRequest!
            }

            try
            {
                var accountCredentials = await this.accountService.CallAuthAPIAccountLogin(bm);
                if (accountCredentials == null)
                {
                    return StatusCode(400, "Wrong credentials or this user doesn't exist!"); // BadRequest!
                }

                return StatusCode(200, accountCredentials); // Ok!
            }
            catch (Exception ex)
            {
                // TO DO: log the exeption here

                return StatusCode(503); // ServiceUnavailable!
            }
        }

        // account/logout
        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutBindingModel bm)
        {
            if (bm == null || bm.Token == null || bm.UserId == null)
            {
                return BadRequest("Token and user id can't be null!");
            }

            try
            {
                var response = await this.accountService.CallAuthAPIAccountLogout(bm);
                if (response)
                {
                    return StatusCode(200); // Ok!
                }
                else
                {
                    return StatusCode(404); // NotFound!
                }
                
            }
            catch (Exception ex)
            {
                // TO DO: log the exeption here

                return StatusCode(503); // ServiceUnavailable!
            }
        }

        [HttpGet]
        [Authorize]
        [Route("test")]
        public IActionResult test()
        {
            return Ok();
        }
    }
}
