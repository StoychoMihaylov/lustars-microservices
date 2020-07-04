namespace ProfileAPI.Models.ViewModels
{
    using System;

    public class UserProfileShortPreviewDataViewModel
    {
        public Guid Id { get; set; }

        public int Credits { get; set; }

        public int LustarLikes { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string AvatarImage { get; set; }

        public GeoLocationShortPreviewDataViewModel GeoLocation { get; set; }
    }
}
