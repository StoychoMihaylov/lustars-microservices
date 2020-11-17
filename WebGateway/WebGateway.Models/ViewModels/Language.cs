namespace WebGateway.Models.ViewModels
{
    using System;

    [Serializable]
    public class Language
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual UserProfileViewModel UserProfile { get; set; }
    }
}
