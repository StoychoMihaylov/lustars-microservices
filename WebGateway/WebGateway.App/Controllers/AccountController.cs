namespace WebGateway.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WebGateway.App.Authorization;
    using WebGateway.Services.Interfaces;
    using WebGateway.Messaging.Interfaces;
    using WebGateway.Models.BidingModels.Account;

    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IAccountBusService accountBusService;
        private readonly IProfileBusService profileBusService;

        public AccountController(
            IAccountService accountService,
            IAccountBusService accountBusService,
            IProfileBusService profileBusService
            )
        {
            this.accountService = accountService;
            this.accountBusService = accountBusService;
            this.profileBusService = profileBusService;
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

            var (registerAccountResponse, registerAccountRejection) = await this.accountBusService.MessageAuthAPI_RegisterAccountProfile(bm);
            if (registerAccountResponse.IsCompletedSuccessfully)
            {
                var credentials = await registerAccountResponse;

                var creteUserResponse = await this.profileBusService.MessageProfileAPI_CreateUserProfile(credentials, bm);
                if (creteUserResponse.Message.IsCreated == true)
                {
                    return StatusCode(201, credentials.Message); // Account and User Created!
                }
                else
                {
                    this.accountBusService.MessageAuthAPI_DeleteAccountProfile(credentials);

                    return StatusCode(501, "Registration faild! Please try again!");
                }
            }
            else
            {       
                var errMessage = await registerAccountRejection;

                return StatusCode(400, errMessage.Message.Value);
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
