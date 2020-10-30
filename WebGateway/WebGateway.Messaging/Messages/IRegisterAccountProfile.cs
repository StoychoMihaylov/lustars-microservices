namespace MessageExchangeContract
{
    public interface IRegisterAccountProfile
    {
        string Name { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string ConfirmPassword { get; set; }
    }
}


