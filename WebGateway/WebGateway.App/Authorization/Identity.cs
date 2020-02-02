namespace WebGateway.App.Utilities
{
    using System;

    using WebGateway.App.Authorization;

    public static class Identity
    {
        private static User user { get; set; }

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
