namespace WebGateway.App.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WebGateway.App.Authorization;
    using WebGateway.Services.Identity;
    using WebGateway.Services.Interfaces;

    [ApiController]
    [Route("profile")]
    public class ChatController : ControllerBase
    {
        private readonly IChatMessangerService chatMessangerService;
        private readonly IProfileService profileService;

        public ChatController(IChatMessangerService chatMessangerService, IProfileService profileService)
        {
            this.chatMessangerService = chatMessangerService;
            this.profileService = profileService;
        }

        [HttpPost]
        [Authorize]
        [Route("open-conversation")]
        public async Task<IActionResult> CreateChatConversation(string id)
        {
            var currentUserId = IdentityManager.CurrentUserId;

            var secondUserId = new Guid();
            var isIdValid = Guid.TryParse(id, out secondUserId);
            if (!isIdValid)
            {
                return StatusCode(400, "invalid guid id format"); // Bad Request
            }

            var isConversationCreated = await this.chatMessangerService.CallProfileAPI_CreateConversationIfUsersLikeEachOther(currentUserId, secondUserId);
            if (isConversationCreated == null)
            {
                return StatusCode(400, "The users need to have liked each other to be able to start conversation");
            }
         
            return StatusCode(201, isConversationCreated); // Created
        }

        [HttpGet]
        [Authorize]
        [Route("conversations")]
        public async Task<IActionResult> GetAllChatConversationForUserById()
        {
            var currentUserId = IdentityManager.CurrentUserId;

            var conversations = await this.chatMessangerService.CallProfileAPI_GetAllChatConversationForUserById(currentUserId);
            if (conversations == null)
            {
                return StatusCode(404); // Not Found
            }

            return StatusCode(200, conversations);
        }

        [HttpGet]
        [Authorize]
        [Route("conversation-messages")]
        public async Task<IActionResult> GetAllChatConversationMessages(string id)
        {
            var conversationId = new Guid();
            var isIdValid = Guid.TryParse(id, out conversationId);
            if (!isIdValid)
            {
                return StatusCode(400); // Bad Request
            }

            var currentUserId = IdentityManager.CurrentUserId;

            var chatMessages = await this.chatMessangerService.CallChatAPI_GetAllConversationMessages(currentUserId, conversationId);
            if (chatMessages != null)
            {
                return StatusCode(200, chatMessages);
            }

            return StatusCode(404);
        }
    }
}
