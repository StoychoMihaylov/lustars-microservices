namespace WebGateway.Models.HubsModels
{
    using System;

    public class ConnectionDataModel
    {
        public Guid ChatId { get; set; }

        public Guid UserAId { get; set; }

        public Guid UserBId { get; set; }

        public string UserAConnectionId { get; set; }

        public string UserBConnectionBId { get; set; }

        public string ConversationGroup { get; set; }
    }
}
