﻿namespace ProfileAPI.Models.BidingModels
{
    using System;
    using ProfileAPI.Data.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public class EditUserProfileBindingModel
    {
        public Guid Id { get; set; }

        public bool EmailNotificationsSubscribed { get; set; }
        public bool IsUserProfileActivated { get; set; }

        // Profile Info
        [MaxLength(15)]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(3000)]
        public string BiographyAndInterests { get; set; }
        public string AvatarImage { get; set; }
        public string FromCity { get; set; }
        public string FromCountry { get; set; }
        [MaxLength(20)]
        public string FeelInMood { get; set; }
        public string LookingFor { get; set; }

        // Social status
        public ICollection<Language> Languages { get; set; }
        public string EducationDegree { get; set; }
        public string University { get; set; }
        public string Work { get; set; }

        // Family status
        public string MeritalStatus { get; set; }
        public bool WantKids { get; set; }
        public bool HaveKids { get; set; }

        // Habits
        public bool DrinkAlcohol { get; set; }
        public string HowOftenDrinkAlcohol { get; set; }
        public bool Smoker { get; set; }
        public string HowOftenSmoke { get; set; }
        public bool DoSport { get; set; }
        public string HowOftenDoSport { get; set; }

        // Body
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Figure { get; set; }

        // About the partner
        public int PartnerAgeRangeFrom { get; set; }
        public int PartnerAgeRangeTo { get; set; }
        public bool PartnerSmoke { get; set; }
        public bool PartnerDrinkAlcohol { get; set; }
        public bool PartnerHaveKids { get; set; }
        public string PartnerFigure { get; set; }
        public bool PartnerDoSport { get; set; }

        // Lustars questions
        // What is most important for you in a partner relationship
        public bool PartnerVisualAppearance { get; set; }
        public bool Trust { get; set; }
        public bool Sex { get; set; }
        public bool FinancialStability { get; set; }
        public bool CommunicationAndUnderstanding { get; set; }
        public bool SameInterests { get; set; }
        public bool OppositeAttracs { get; set; }
        public bool GrowingFamily { get; set; }
        public bool LoveForAnimals { get; set; }
        public bool ShareSameReligion { get; set; }
        public bool KeepTraditions { get; set; }
    }
}
