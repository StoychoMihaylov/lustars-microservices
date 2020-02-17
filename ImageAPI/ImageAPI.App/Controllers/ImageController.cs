namespace ImageAPI.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("image")]
    public class ImageController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
