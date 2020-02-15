namespace WebGateway.App.Infrastructure.Authorization
{
    using System;

    public static class IdentityManager
    {
        private static User user = new User();

        public static Guid CurrentUserId
        {
            get 
            {
                if (user.Id == Guid.Empty)
                {
                    throw new Exception("User 'id' need to be set before try to access it!");
                }

                return user.Id;
            }
        }

        public static string CurrentUserToken
        {
            get
            {
                if (user.Token == string.Empty)
                {
                    throw new Exception("User 'token' need to be set before try to access it!");
                }

                return user.Token; 
            }
        }

        public static void SetCurrentUser(Guid userId, string userToken)
        {
            if (userId == Guid.Empty || userToken == string.Empty)
            {
                throw new Exception("Current 'user' data input is not valid!");
            }

            user.Id = userId;
            user.Token = userToken;
        }
    }
}
