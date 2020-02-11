namespace WebGateway.App.Infrastructure.Authorization
{
    using System;

    public static class IdentityManager
    {
        private static User user = new User();

        public static User GetCurrentUser()
        {
            return user;
        }

        public static void SetCurrentUserId(Guid id)
        {
            user.Id = id;
        }

        public static void SetCurrentUserToken(string token)
        {
            user.Token = token;
        }
    }
}
