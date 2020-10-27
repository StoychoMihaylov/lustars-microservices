namespace WebGateway.App.Controllers
{
    using System;
    using MassTransit;
    using Message.Contract;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WebGateway.App.Authorization;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Account;
    using WebGateway.Models.BidingModels.UserProfile;

    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IProfileService profileService;
        private readonly IBus bus;

        public AccountController(IAccountService accountService, IProfileService profileService, IBus bus)
        {
            this.accountService = accountService;
            this.profileService = profileService;
            this.bus = bus;
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
                var endPoint = await this.bus.GetSendEndpoint(new Uri("queue:register-new-account"));

                await endPoint.Send<IRegisterNewAccountMessage>(bm);

                //await this.bus.Publish<IRegisterNewAccountMessage>(bm);

                //var accountCredentials = await this.accountService.CallAuthAPI_AccountRegister(bm);
                //if (accountCredentials == null)
                //{
                //    return StatusCode(400, "Email already exists or wrong credentials!"); // BadRequest!
                //}

                //var userProfileVm = new CreateUserProfileBindingModel()
                //{
                //    Id = accountCredentials.UserId,
                //    Name = bm.Name,
                //    Gender = bm.Gender,
                //    Email = bm.Email
                //};

                //var isCreated = await this.profileService.CallProfileAPI_CreateUserProfile(userProfileVm); // Call to ProfileAPI

                //if (!isCreated)
                //{
                //    this.accountService.CallAuthAPI_DeleteAccount(accountCredentials); // Revert account creation(delete it)
                //    return StatusCode(503); // ServiceUnavailable!
                //}

                //return StatusCode(201, accountCredentials); // Created!

                return StatusCode(200, "Created!");
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
                var accountCredentials = await this.accountService.CallAuthAPI_AccountLogin(bm);
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
                var response = await this.accountService.CallAuthAPI_AccountLogout(bm);
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
    }
}
