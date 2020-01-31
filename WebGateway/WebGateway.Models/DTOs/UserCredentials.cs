namespace WebGateway.Models.DTOs
{
    using System;

    public class UserCredentials
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }
    }
}
