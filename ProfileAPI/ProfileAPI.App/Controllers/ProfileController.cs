namespace ProfileAPI.App.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("profile")]
    public class ProfileController : ControllerBase
    {
        public ProfileController()
        {

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Profile(Guid id)
        {
            if (id == null)
            {
                return StatusCode(404); // NotFound!
            }

            var someUser = "pesho";

            return StatusCode(200, someUser); //Ok!
        }
    }
}
