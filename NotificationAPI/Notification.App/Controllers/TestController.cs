namespace Notification.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("notification")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("test-me")]
        public IActionResult GetME()
        {
            return StatusCode(200, ";)");
        }
    }
}