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
                return BadRequest(ModelState);
            }

            if (bm.Password != bm.ConfirmPassword)
            {
                return BadRequest("Invalid credentials!");
            }

            try
            {
                var response = await this.service.CallAuthAPIAccountRegister(bm);

                return Ok(response);
            }
            catch (Exception ex)
            {
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
                return BadRequest(ex.Message);
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
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
