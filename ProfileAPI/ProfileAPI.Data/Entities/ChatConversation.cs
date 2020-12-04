namespace ProfileAPI.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChatConversation
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ChatStarterUserId { get; set; }
        public virtual UserProfile UserChatStarter { get; set; }

        public Guid InvitedUserId { get; set; }
        public virtual UserProfile InvitedUser { get; set; }

        public DateTime StartedOn { get; set; }
    }
}
