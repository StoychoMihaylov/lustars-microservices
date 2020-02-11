﻿namespace AuthAPI.App.Controllers
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
                return StatusCode(400, "User with this email already exist!"); // BadRequest!
            }

            var userCredentials = this.service.CreateNewUserAccount(bm); // User created, will return token(loged-in automaticaly)

            return StatusCode(201, userCredentials); // Created!
        }

        // account/login
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginUserBindingModel bm)
        {
            var userCredentials = this.service.LoginUser(bm);

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
    }
}
