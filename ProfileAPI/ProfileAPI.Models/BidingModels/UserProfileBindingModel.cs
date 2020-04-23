namespace ProfileAPI.Models.BidingModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserProfileBindingModel
    {
        public Guid Id { get; set; }
        public bool EmailNotificationsSubscribed { get; set; }
        public bool IsUserProfileActivated { get; set; }

        // Profile Info
        [MaxLength(15)]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [MaxLength(20)]
        public string Title { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LookingFor { get; set; }

        // About me
        [MaxLength(3000)]
        public string Biography { get; set; }
        public string EducationDegree { get; set; }
        public string University { get; set; }
        public string Work { get; set; }
        public string Languages { get; set; }
        public bool WantToHaveKids { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Figure { get; set; }
        public string WantKids { get; set; }
        public bool HaveKids { get; set; }
        public bool DrinkAlcohol { get; set; }
        public string HowOftenDrinkAlcohol { get; set; }
        public bool Smoker { get; set; }
        public string HowOftenSmoke { get; set; }
        public int Income { get; set; }
        public string MeritalStatus { get; set; }

        // About the partner
        public int PartnerAgeRangeFrom { get; set; }
        public int PartnerAgeRangeTo { get; set; }
        public int PartnerIncomeFrom { get; set; }
        public int PartnerIncomeTo { get; set; }
        public bool PartnerSmoke { get; set; }
        public bool PartnerDrinkAlcohol { get; set; }
        public bool PartnerHaveKids { get; set; }
        public string PartnerFigure { get; set; }
    }
}
