namespace ProfileAPI.Models.ViewModels
{
    using System;

    public class ChatConversationsViewModel
    {
        public Guid Id { get; set; }

        public Guid ChatStarterUserId { get; set; }

        public Guid InvitedUserId { get; set; }

        public DateTime StartedOn { get; set; }

        public string CorresponderAvatarImage { get; set; }

        public string CorresponderNames { get; set; }
    }
}
