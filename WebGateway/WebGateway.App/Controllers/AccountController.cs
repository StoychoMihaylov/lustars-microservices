namespace WebGateway.App.Controllers
{
    using System;
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using Microsoft.AspNetCore.Mvc;
    using WebGateway.App.Authorization;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Account;

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
                //var endPoint = await this.bus.GetSendEndpoint(new Uri("queue:register-new-account"));

                //await endPoint.Send<IRegisterNewAccountMessage>(bm);

                //await this.bus.Publish<IRegisterNewAccountMessage>(bm);

                var authAPI = this.bus.CreateRequestClient<IRegisterAccountProfile>(new Uri("queue:register-account-profile-queue"), TimeSpan.FromSeconds(30));
                var profileAPI = this.bus.CreateRequestClient<ICreateUserProfile>(new Uri("queue:create-user-profile-queue"), TimeSpan.FromSeconds(30));

                var (accountResponse, accountRejection) = await authAPI.GetResponse<IAccountCredentials, IRegisterAccountRejection>(bm);
                if (accountResponse.IsCompletedSuccessfully)
                {
                    var credentials = await accountResponse;

                    var userResponse = await profileAPI.GetResponse<IUserProfileCreated>(new
                    {
                        Id = credentials.Message.UserId,
                        Name = bm.Name,
                        Gender = bm.Gender,
                        Email = bm.Email,
                    });

                    if (userResponse.Message.IsCreated == true)
                    {
                        return StatusCode(200, credentials.Message);
                    }
                }
                else
                {
                    var errMessage = await accountRejection;

                    return StatusCode(400, errMessage.Message.Value);
                }

                return StatusCode(501); // Not Implemented!

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
