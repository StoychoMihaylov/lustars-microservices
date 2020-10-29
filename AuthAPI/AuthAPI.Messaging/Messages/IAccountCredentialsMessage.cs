namespace MessageExchangeContract
{
    using System;

    public interface IAccountCredentialsMessage
    {
        string OrderId { get; }

        Guid UserId { get; set; }

        string Token { get; set; }

        string Name { get; set; }

        string Email { get; set; }
    }
}
