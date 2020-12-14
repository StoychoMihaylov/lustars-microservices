namespace WebGateway.Services.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using WebGateway.Services.Common;
    using WebGateway.Services.Endpoints;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Chat;

    public class ChatMessangerService : Service, IChatMessangerService
    {
        public ChatMessangerService(HttpClient httpClient, StringContentSerializer stringContentSerializer)
           : base(httpClient, stringContentSerializer) { }

        public async Task<string> CallChatAPI_GetAllConversationMessages(Guid currentUserId, Guid conversationId)
        {
            var response = await this.HttpClient.GetAsync(ChatAPIService.Endpoint + $"chat/{currentUserId}/conversation/{conversationId}/messages");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CallProfileAPI_CreateConversationIfUsersLikeEachOther(Guid currentUserId, Guid secondUserId)
        {
            var chatConversation = new ChatConversationBm()
            {
                CurrentUserID = currentUserId,
                UserToStartConversationWithID = secondUserId
            };

            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(chatConversation);

            var response = await this.HttpClient.PostAsync(ProfileAPIService.Endpoint + $"profile/open-conversation", stringContent);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CallProfileAPI_GetAllChatConversationForUserById(Guid currentUserId)
        {
            var response = await this.HttpClient.GetAsync(ProfileAPIService.Endpoint + $"profile/{currentUserId}/conversations");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }
    }
}
