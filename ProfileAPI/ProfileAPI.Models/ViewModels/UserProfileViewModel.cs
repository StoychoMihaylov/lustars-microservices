namespace ProfileAPI.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ProfileAPI.Data.Entities;

    public class UserProfileViewModel
    {
        [MaxLength(15)]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedOn { get; set; }

        [MaxLength(3000)]
        public string Biography { get; set; }

        public string City { get; set; }

        public int Credits { get; set; }

        public int Superlikes { get; set; }

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

        public ICollection<Image> Images { get; set; }

        public ICollection<GeoLocation> GeoLocations { get; set; }
    }
}
