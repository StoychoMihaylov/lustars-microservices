namespace Notification.App.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Notification.App.Hubs.Web;
    using Microsoft.AspNetCore.SignalR;

    [ApiController]
    [Route("notification")]
    public class TestController : ControllerBase
    {
        private readonly IHubContext<WebNotificationsHub> webNotificationHub;

        public TestController(IHubContext<WebNotificationsHub> webNotificationHub)
        {
            this.webNotificationHub = webNotificationHub;
        }

        [HttpGet]
        [Route("test-me")]
        public async Task<IActionResult> GetME()
        {
            await webNotificationHub.Clients.All.SendAsync("Notification", "Test");

            return StatusCode(200, ";)");
        }
    }
}