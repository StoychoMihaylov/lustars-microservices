namespace WebGateway.Models.ViewModels
{
    using System;

    public class GeoLocation
    {
        public long Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; } 

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public UserProfileViewModel UserProfile { get; set; }
    }
}
