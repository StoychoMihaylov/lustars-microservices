namespace ProfileAPI.Models.ViewModels
{
    using System;
    using ProfileAPI.Data.Entities;
    using System.Collections.Generic;

    public class UserProfileDetailedDataViewModel
    {
        public Guid Id { get; set; }

        // Settings info
        public int Credits { get; set; }
        public int Superlikes { get; set; }
        public string Email { get; set; }
        public bool EmailNotificationsSubscribed { get; set; }
        public bool IsUserProfileActivated { get; set; }

        // Profile Info
        public string Name { get; set; }
        public string LastName { get; set; }
        public string AvatarImage { get; set; }
        public string FromCity { get; set; }
        public string FromCountry { get; set; }
        public string FeelInMood { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LookingFor { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<Image> Images { get; set; }
        public GeoLocation GeoLocation { get; set; }

        // About me
        public string Biography { get; set; }
        public ICollection<Language> Languages { get; set; }
        public string EducationDegree { get; set; }
        public string University { get; set; }
        public string Work { get; set; }
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
        public string Income { get; set; }
        public string MeritalStatus { get; set; }

        // About the partner
        public int PartnerAgeRangeFrom { get; set; }
        public int PartnerAgeRangeTo { get; set; }
        public string PartnerIncome { get; set; }
        public bool PartnerSmoke { get; set; }
        public bool PartnerDrinkAlcohol { get; set; }
        public bool PartnerHaveKids { get; set; }
        public string PartnerFigure { get; set; }
    }
}
