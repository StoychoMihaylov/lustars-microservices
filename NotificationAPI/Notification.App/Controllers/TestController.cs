namespace Notification.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Notification.App.Hubs.Web;
    using Microsoft.AspNetCore.SignalR;
    using Notification.App.Hubs.Interfaces;

    [ApiController]
    [Route("notification")]
    public class TestController : ControllerBase
    {
        private readonly IHubContext<WebNotificationHub> webNotificationHub;
        private readonly IWebEventNotification webEventNotification;

        public TestController(IHubContext<WebNotificationHub> webNotificationHub, IWebEventNotification webEventNotification)
        {
            this.webNotificationHub = webNotificationHub;
            this.webEventNotification = webEventNotification;
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

            await this.webEventNotification.PushWebEventNotification(userId, message);    

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