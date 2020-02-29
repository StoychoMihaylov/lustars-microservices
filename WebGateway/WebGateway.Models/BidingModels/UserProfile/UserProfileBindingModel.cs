namespace WebGateway.Models.BidingModels.UserProfile
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserProfileBindingModel
    {
        public Guid Id { get; set; }

        [MaxLength(15)]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(3000)]
        public string Biography { get; set; }

        public string City { get; set; }

        public string LookingFor { get; set; }

        public int AgeRangeFrom { get; set; }

        public int AgeRangeTo { get; set; }

        public bool WantToHaveKids { get; set; }

        public string EducationDegree { get; set; }

        public string University { get; set; }

        public string Work { get; set; }

        public string Languages { get; set; }

        public bool EmailNotificationsSubscribe { get; set; }

        public bool IsActivated { get; set; }
    }
}
