namespace ProfileAPI.Models.BidingModels
{
    using System;

    public class ChatConversationBindingModel
    {
        public Guid CurrentUserID { get; set; }

        public Guid UserToStartConversationWithID { get; set; }
    }
}
