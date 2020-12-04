namespace WebGateway.Models.BidingModels.Chat
{
    using System;

    public class ChatConversationBm
    {
        public Guid CurrentUserID { get; set; }

        public Guid UserToStartConversationWithID { get; set; }
    }
}
