﻿namespace ProfileAPI.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GeoLocation
    {
        [Key]
        public long Id { get; set; }

        public string Street { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
