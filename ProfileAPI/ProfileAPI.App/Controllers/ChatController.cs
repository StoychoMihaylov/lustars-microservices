namespace ProfileAPI.App.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Services.Interfaces;

    [ApiController]
    [Route("profile")]
    public class ChatController : ControllerBase
    {
        private IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpPost]
        [Route("open-conversation")]
        public IActionResult CreateChatConversation(ChatConversationBindingModel bm)
        {
            var like = this.chatService.CheckIfUsersLikeEachOther(bm);
            if (like)
            {
                var existedConversationId = this.chatService.ChechIfConversationBetweenThoseUsersAlreadyExist(bm);
                if (existedConversationId != null)
                {
                    return StatusCode(201, existedConversationId);
                }

                var conversationId = this.chatService.CreateChatConversation(bm);

                return StatusCode(201, conversationId); // Created!
            }

            return StatusCode(501); // Not Implemented! 
        }

        [HttpGet]
        [Route("{id}/conversations")]
        public IActionResult GetAllChatConversations(Guid id)
        {
            var conversations = this.chatService.GetAllChatConversationsForUserById(id);
            if (conversations.Count == 0)
            {
                return StatusCode(404); // Not Found!
            }

            return StatusCode(200, conversations);
        }
    }
}
