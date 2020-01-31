namespace WebGateway.Models.ViewModels
{
    using System;

    public class AccountCredentialsViewModel
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
