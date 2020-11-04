namespace AuthAPI.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using AuthAPI.Models.ViewModels;
    using AuthAPI.Models.BidingModels;
    using AuthAPI.Services.Interfaces;

    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;

        public AccountController( IAccountService service)
        {
            this.service = service;
        }

        // account/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserBindingModel bm)
        {
            var userCredentials = await this.service.LoginUser(bm);

            if (userCredentials == null)
            {
                return StatusCode(400, "Wrong credentials!"); // BadRequest!
            }

            return StatusCode(200, userCredentials); // Ok!
        }

        // account/logout
        [HttpPost]
        [Route("logout")]
        public IActionResult Logout([FromBody] LogoutBindingModel bm)
        {
            try
            {
                this.service.DeleteUserToken(bm);
            }
            catch
            {
                return StatusCode(404); // NotFound!
            }

            return StatusCode(200); // Ok!
        }

        // account/authorized
        [HttpPost]
        [Route("authorized")]
        public IActionResult CheckIfUserIsAuthorized([FromBody] Token token)
        {
            var userCredentials = this.service.CheckIfTokenIsValidAndReturnUserCredentials(token);

            if (userCredentials == null)
            {
                return StatusCode(401); // Unauthorized!
            }

            return StatusCode(200, userCredentials); // Ok!
        }

        // There is MassTransit implementation as well
        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteUser([FromBody] AccountCredentialsViewModel accountCredentialsVm)
        {
            if (accountCredentialsVm.UserId == Guid.Empty)
            {
                return StatusCode(400, "Invalid data input format! user id can't be empty guid!");
            }

            if (accountCredentialsVm == null)
            {
                return StatusCode(400, "Invalid data input format!");
            }

            var isUserDeleted = this.service.DeleteUser(accountCredentialsVm);
            if (!isUserDeleted)
            {
                return StatusCode(501); //  NotImplemented!
            }

            return StatusCode(202); // Accepted!
        }
    }
}
