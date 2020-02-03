namespace AuthAPI.App.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using AuthAPI.Models.BidingModels;
    using AuthAPI.Services.Interfaces;

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

        /// SOOOOOOOOOOOOOOOME CHANGE HERE!!!
       
        // account/register
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterAndLogin([FromBody] RegisterUserBindingModel bm)
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

            var userAlreadyExist = this.service.CheckIfUserExist(bm);

            if (userAlreadyExist)
            {
                logger.LogError($"registration fail the user already exist");

                return BadRequest("User with this email already exist!");
            }

            var userCredentials = this.service.CreateNewUserAccount(bm); // User created, will return token(loged-in automaticaly)

            if (userCredentials == null)
            {
                logger.LogError("Error on registration token has been not returned");

                return BadRequest();
            }

            logger.LogInformation("User has been registered! {time}", DateTime.UtcNow);

            // created!
            return Ok(userCredentials);
        }

        // account/login
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginUserBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning($"Invalid model state on log-in with email:{bm.Email}");

                return BadRequest(ModelState);
            }

            var userCredentials = this.service.LoginUser(bm);

            if (userCredentials == null)
            {
                logger.LogWarning($"Wrong credentials on log-on with email:{bm.Email}");

                return BadRequest("Wrong credentials!");
            }

            logger.LogInformation($"User loged-in with email:{bm.Email}");

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
            catch (Exception ex)
            {
                logger.LogError(ex, "token for user with id:{userId} was not found on log-out", bm.UserId);

                return NotFound();
            }

            logger.LogInformation("user with id:{userId} successful log-outed", bm.UserId);

            return Ok();
        }

        // account/authorized
        [HttpPost]
        [Route("authorized")]
        public IActionResult CheckIfUserIsAuthorized([FromBody] Token token)
        {
            if (token == null)
            {
                return BadRequest("No token provided!");
            }

            var userCredentials = this.service.CheckIfTokenIsValidAndReturnUserCredentials(token);

            if (userCredentials == null)
            {
                return Unauthorized();
            }

            return Ok(userCredentials);
        }
    }
}
