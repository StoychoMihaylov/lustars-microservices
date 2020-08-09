

namespace ProfileAPI.Models.ViewModels
{
    using System;
    using ProfileAPI.Data.Entities;

    public class UserProfileVisitorViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Age { get; set; }

        public string NameAndAge
        {
            get
            {
                return $"{Name}{AgeReplacement()}";
            }
        }

        public string AvatarImage { get; set; }

        public string Distance { get; set; }

        public string Location
        {
            get
            {
                return $"{City}, {Country}";
            }
        }

        public int CountImages { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public GeoLocation GeoLocation { get; set; }

        private string AgeReplacement()
        {
            return Age != string.Empty ? Age : "";
        }
    }
}
