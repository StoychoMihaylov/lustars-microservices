namespace ProfileAPI.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserProfile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(15)]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int AgeRange { get; set; }

        [MaxLength(2500)]
        public string Biography { get; set; }

        public string City { get; set; }

        public int Credits { get; set; }

        public int Superlikes { get; set; }

        public string LookingFor { get; set; }

        public bool WantToHaveKids { get; set; }

        public string EducationDegree { get; set; }

        public string University { get; set; }

        public string Work { get; set; }

        public string Languages { get; set; }

        public bool EmailNotificationsSubscribe { get; set; }

        public bool IsActivated { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
