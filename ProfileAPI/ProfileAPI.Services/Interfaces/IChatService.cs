namespace ProfileAPI.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ProfileAPI.Models.ViewModels;
    using ProfileAPI.Models.BidingModels;

    public interface IChatService
    {
        bool CheckIfUsersLikeEachOther(ChatConversationBindingModel bm);
        Guid CreateChatConversation(ChatConversationBindingModel bm);
        Guid? ChechIfConversationBetweenThoseUsersAlreadyExist(ChatConversationBindingModel bm);
        List<ChatConversationsViewModel> GetAllChatConversationsForUserById(Guid id);
    }
}
