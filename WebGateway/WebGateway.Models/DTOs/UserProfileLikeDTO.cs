namespace WebGateway.Models.DTOs
{
    using System;

    public class UserProfileLikeDTO
    {
        public Guid LikeFrom { get; set; }

        public Guid LikeTo { get; set; }
    }
}
