﻿namespace WebGateway.App.Infrastructure.Authorization
{
    using System;

    // Every time when AuthAttributeService is executed it will set the current user id and token in cache
    public static class IdentityManager
    {
        private static Guid userId;

        private static string userToken;

        public static Guid CurrentUserId
        {
            get 
            {
                if (userId == Guid.Empty)
                {
                    throw new Exception("User 'id' need to be set before try to access it!");
                }

                return userId;
            }
        }

        public static string CurrentUserToken
        {
            get
            {
                if (userToken == string.Empty)
                {
                    throw new Exception("User 'token' need to be set before try to access it!");
                }

                return userToken; 
            }
        }

        public static void SetCurrentUser(Guid id, string token)
        {
            if (id == Guid.Empty || token == string.Empty)
            {
                throw new Exception("Current 'user' data input is not valid!");
            }

            userId = id;
            userToken = token;
        }
    }
}
