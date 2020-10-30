namespace MessageExchangeContract
{
    using System;

    public interface ICreateNewUserProfile
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Gender { get; set; }

        string Email { get; set; }
    }
}