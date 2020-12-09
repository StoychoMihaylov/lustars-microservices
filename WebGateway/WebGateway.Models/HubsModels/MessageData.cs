namespace WebGateway.Models.HubsModels
{
    using System;

    public class MessageData
    {
        public Guid ConversationId { get; set; }

        public Guid Sender { get; set; }

        public Guid Recipient { get; set; }

        public DateTime SendOn { get; set; }

        public string Content { get; set; }
    }
}
