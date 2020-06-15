﻿namespace ProfileAPI.Models.ViewModels
{
    using System;
    using ProfileAPI.Data.Entities;

    public class UserProfileInDistanceViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string AvatarImage { get; set; }

        public GeoLocation GeoLocation { get; set; }
    }
}