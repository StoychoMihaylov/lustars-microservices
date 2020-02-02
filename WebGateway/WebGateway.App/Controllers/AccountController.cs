namespace WebGateway.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using WebGateway.App.Utilities;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Account;

    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        private readonly IAccountService service;

        public AccountController(ILogger<AccountController> logger, IAccountService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // account/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAndLogin([FromBody] RegisterUserBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("invalid model state on registrations {time}", DateTime.UtcNow);
                return BadRequest(ModelState);
            }

            if (bm.Password != bm.ConfirmPassword)
            {
                logger.LogWarning("incorrect password and confirm password on registrations {time}", DateTime.UtcNow);
                return BadRequest("Invalid credentials!");
            }

            try
            {
                var response = await this.service.CallAuthAPIAccountRegister(bm);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "AuthAPIAccountRegister {time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }  
        }

        // account/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning($"Invalid model state on log-in with email:{bm.Email}");
                return BadRequest(ModelState);
            }

            try
            {
                var response = await this.service.CallAuthAPIAccountLogin(bm);
                if (response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("on login:" + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // account/logout
        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutBindingModel bm)
        {
            try
            {
                var response = await this.service.CallAuthAPIAccountLogout(bm);
                if (response)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "token for user with id:{userId} was not found on log-out", bm.UserId);
                return BadRequest();
            }
        }
    }
}
