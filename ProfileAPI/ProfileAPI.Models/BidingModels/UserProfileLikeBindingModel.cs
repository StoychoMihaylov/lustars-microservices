namespace ProfileAPI.Models.BidingModels
{
    using System;

    public class UserProfileLikeBindingModel
    {
        public Guid LikeFrom { get; set; }

        public Guid LikeTo { get; set; }
    }
}
