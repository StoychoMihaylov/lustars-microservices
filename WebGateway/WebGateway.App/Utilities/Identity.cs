namespace WebGateway.App.Utilities
{
    using System;

    public static class Identity
    {
        public static Guid Id { get; set; }

        public static Guid CurrentUser()
        {
            return Id;
        }
    }
}
