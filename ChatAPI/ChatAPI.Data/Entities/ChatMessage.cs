namespace ChatAPI.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ChatMessage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid ConversationId { get; set; } // Indexed in the model builder

        public Guid Sender { get; set; }

        public Guid Recipient { get; set; }

        public DateTime SendOn { get; set; }

        public string Content { get; set; }
    }
}
