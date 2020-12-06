﻿namespace ProfileAPI.Services.Interfaces
{
    using System;
    using ProfileAPI.Data.Entities;
    using System.Collections.Generic;
    using ProfileAPI.Models.BidingModels;

    public interface IChatService
    {
        bool CheckIfUsersLikeEachOther(ChatConversationBindingModel bm);
        Guid CreateChatConversation(ChatConversationBindingModel bm);
        Guid? ChechIfConversationBetweenThoseUsersAlreadyExist(ChatConversationBindingModel bm);
        List<ChatConversation> GetAllChatConversationsForUserById(Guid id);
    }
}
