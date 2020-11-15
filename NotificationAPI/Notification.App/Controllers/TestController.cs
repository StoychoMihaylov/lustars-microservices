namespace Notification.App.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Notification.App.Hubs.Web;
    using Microsoft.AspNetCore.SignalR;

    [ApiController]
    [Route("notification")]
    public class TestController : ControllerBase
    {
        private readonly IHubContext<WebNotificationHub> webNotificationHub;

        public TestController(IHubContext<WebNotificationHub> webNotificationHub)
        {
            this.webNotificationHub = webNotificationHub;
        }

        [HttpGet]
        [Route("test-me")]
        public async Task<IActionResult> GetME(Guid userId)
        {
            var generator = new Random();
            var messages = new string[] 
            { 
                "Dobre sme ama sh se opraim!", 
                "Kak si babe?",
                "Just go fuck yourself bro ;)",
                "What's up man ?",
                "No, no thanks man!"
            };
            var message = messages[generator.Next(0, 4)];

            var connectionIds = WebNotificationHub
                .UserAndConnectionIds
                .FirstOrDefault(x => x.Key == userId)
                .Value;

            foreach (var id in connectionIds)
            {
                await this.webNotificationHub
                    .Clients
                    .Client(id)
                    .SendAsync("user-web-event-notification", message);
            }

            return StatusCode(200, ";)");
        }

        [HttpGet]
        [Route("check-hub-conections")]
        public async Task<IActionResult> ChechHubConections()
        {
            var result = WebNotificationHub.UserAndConnectionIds;

            return StatusCode(200, result.Keys);
        }
    }
}