namespace MessageExchangeContract
{
    using System;

    public interface ICreateUserProfile
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Gender { get; set; }

        string Email { get; set; }
    }
}