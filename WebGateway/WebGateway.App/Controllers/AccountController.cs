namespace WebGateway.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using WebGateway.App.Utilities;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Account;

    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
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
                var accountCredentials = await this.service.CallAuthAPIAccountRegister(bm);
                if (accountCredentials != null)
                {
                    return StatusCode(201, accountCredentials); // Created!
                }
                else
                {
                    return StatusCode(400, "Email already exists or wrong credentials!"); // BadRequest!
                }
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
                var accountCredentials = await this.service.CallAuthAPIAccountLogin(bm);
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
                var response = await this.service.CallAuthAPIAccountLogout(bm);
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
