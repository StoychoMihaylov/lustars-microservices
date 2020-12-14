namespace ChatAPI.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ChatAPI.Services.Interfaces;

    [ApiController]
    [Route("chat")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        [Route("{userId}/conversation/{conversationId}/Messages")]
        public async Task<IActionResult> GetAllConversationMessages(Guid userId, Guid conversationId)
        {
            var messages = await this.messageService.GetAllConversationMessages(userId, conversationId);
            if (messages != null)
            {
                return StatusCode(200, messages);
            }

            return StatusCode(404);
        }
    }
}
