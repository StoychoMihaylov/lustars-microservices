namespace WebGateway.Models.BidingModels.UserProfile
{
    using System;

    public class CreateUserProfileBindingModel
    {
        public Guid Id { get; set; }
       
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }
    }
}
