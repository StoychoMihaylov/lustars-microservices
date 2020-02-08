namespace AuthAPI.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
       
        // account/register
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterAndLogin([FromBody] RegisterUserBindingModel bm)
        {
            var userAlreadyExist = this.service.CheckIfUserExist(bm);

            if (userAlreadyExist)
            {
                return BadRequest("User with this email already exist!");
            }

            var userCredentials = this.service.CreateNewUserAccount(bm); // User created, will return token(loged-in automaticaly)

            // created!
            return Ok(userCredentials);
        }

        // account/login
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginUserBindingModel bm)
        {
            var userCredentials = this.service.LoginUser(bm);

            if (userCredentials == null)
            {
                return BadRequest("Wrong credentials!");
            }

            return Ok(userCredentials);
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
                return NotFound();
            }

            return Ok();
        }

        // account/authorized
        [HttpPost]
        [Route("authorized")]
        public IActionResult CheckIfUserIsAuthorized([FromBody] Token token)
        {
            var userCredentials = this.service.CheckIfTokenIsValidAndReturnUserCredentials(token);

            if (userCredentials == null)
            {
                return Unauthorized();
            }

            return Ok(userCredentials);
        }
    }
}
