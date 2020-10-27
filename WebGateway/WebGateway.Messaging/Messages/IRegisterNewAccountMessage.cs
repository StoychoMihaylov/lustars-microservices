namespace Message.Contract
{
    public interface IRegisterNewAccountMessage
    {
        string Name { get; set; }

        string Gender { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string ConfirmPassword { get; set; }
    }
}


