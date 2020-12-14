namespace ProfileAPI.Services.Services
{
    using System;
    using System.Linq;
    using ProfileAPI.Data.Entities;
    using System.Collections.Generic;
    using ProfileAPI.Data.Interfaces;
    using ProfileAPI.Models.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Services.Interfaces;

    public class ChatService : Service, IChatService
    {
        public ChatService(IProfileDBContext context)
          : base(context)
        { }

        public bool CheckIfUsersLikeEachOther(ChatConversationBindingModel bm)
        {
            var doTheyLikeEachOther = this.Context
                .Likes
                .Where(like =>
                    (like.LikeFromId == bm.CurrentUserID && like.LikeToId == bm.UserToStartConversationWithID) ||
                    (like.LikeFromId == bm.UserToStartConversationWithID && like.LikeToId == bm.CurrentUserID)
                ).ToList();

            if (doTheyLikeEachOther.Count == 2) // Both users like each other
            {
                return true;
            }

            return false;
        }

        public Guid CreateChatConversation(ChatConversationBindingModel bm)
        {
            var invitedUser = this.Context
                .UserProfiles
                .Include(u => u.ChatIvitations)
                .Where(u => u.Id == bm.UserToStartConversationWithID)
                .FirstOrDefault();

            if (invitedUser == null)
                throw new Exception($"Chat invitaion faild because user with id:{bm.UserToStartConversationWithID} can't be found!");

            var chatConversation = new ChatConversation()
            {
                Id = Guid.NewGuid(),
                ChatStarterUserId = bm.CurrentUserID,
                InvitedUserId = bm.UserToStartConversationWithID,
                StartedOn = DateTime.UtcNow
            };

            var currentUser = this.Context
                .UserProfiles
                .Include(u => u.StartedChatConversations)
                .Where(u => u.Id == bm.CurrentUserID)
                .FirstOrDefault();

            currentUser.StartedChatConversations.Add(chatConversation);
            invitedUser.ChatIvitations.Add(chatConversation);

            this.Context.UserProfiles.UpdateRange(invitedUser, currentUser);
            this.Context.SaveChanges();

            return chatConversation.Id;
        }

        public Guid? ChechIfConversationBetweenThoseUsersAlreadyExist(ChatConversationBindingModel bm)
        {
            var exist = this.Context
                .ChatConversations
                .Where(chat =>
                    (chat.ChatStarterUserId == bm.CurrentUserID && chat.InvitedUserId == bm.UserToStartConversationWithID) ||
                    (chat.InvitedUserId == bm.CurrentUserID && chat.ChatStarterUserId == bm.UserToStartConversationWithID)
                )
                .FirstOrDefault();

            if (exist != null)
            {
                return exist.Id;
            }

            return null;
        }

        public List<ChatConversationsViewModel> GetAllChatConversationsForUserById(Guid id)
        {
            var chatConversations = new List<ChatConversationsViewModel>();

            var userStartedConversations = this.Context
                .ChatConversations
                .Include(chat => chat.InvitedUser)
                .Select(chat => new ChatConversationsViewModel()
                {
                    Id = chat.Id,
                    ChatStarterUserId = chat.ChatStarterUserId,
                    InvitedUserId = chat.InvitedUserId,
                    StartedOn = chat.StartedOn,
                    CorresponderAvatarImage = chat.InvitedUser.AvatarImage,
                    CorresponderNames = $"{chat.InvitedUser.Name} {chat.InvitedUser.LastName}"
                })
                .Where(chat =>
                    chat.ChatStarterUserId == id
                )
                .ToList();

            var userChatInvitations = this.Context
                .ChatConversations
                .Include(chat =>
                    chat.UserChatStarter
                )
                .Select(chat => new ChatConversationsViewModel() 
                { 
                    Id = chat.Id,
                    ChatStarterUserId = chat.ChatStarterUserId,
                    InvitedUserId = chat.InvitedUserId,
                    StartedOn = chat.StartedOn,
                    CorresponderAvatarImage = chat.UserChatStarter.AvatarImage,
                    CorresponderNames = $"{chat.UserChatStarter.Name} {chat.UserChatStarter.LastName}"
                })
                .Where(chat =>
                    chat.InvitedUserId == id
                )
                .ToList();

            chatConversations.AddRange(userStartedConversations);
            chatConversations.AddRange(userChatInvitations);

            return chatConversations;
        }
    }
}
