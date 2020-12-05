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
        private readonly IProfileService profileService;

        public ChatController(IProfileService profileService)
        {
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

            var isConversationCreated = await this.profileService.CallProfileAPI_CreateConversationIfUsersLikeEachOther(currentUserId, secondUserId);
            if (isConversationCreated == null)
            {
                return StatusCode(400, "The users need to have liked each other to be able to start conversation");
            }
         
            return StatusCode(201, isConversationCreated); // Created
        }
    }
}
