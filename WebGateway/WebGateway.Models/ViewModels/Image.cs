﻿namespace WebGateway.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        [Key]
        public long Id { get; set; }

        public string Url { get; set; }

        public DateTime UploadedOn { get; set; }

        public virtual UserProfileViewModel UserProfile { get; set; }
    }
}
