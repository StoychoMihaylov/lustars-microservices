namespace MessageExchangeContract
{
    using System;

    public interface IAccountCredentials
    {
        Guid UserId { get; set; }

        string Token { get; set; }

        string Name { get; set; }

        string Email { get; set; }
    }
}