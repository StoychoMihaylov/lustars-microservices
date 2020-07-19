using System;

namespace ProfileAPI.Data.Entities
{
    public class Like
    {
        public Guid LikeFromId { get; set; }
        public virtual UserProfile LikeFrom { get; set; }

        public Guid LikeToId { get; set; }
        public virtual UserProfile LikeTo { get; set; }

        public DateTime onDate { get; set; }
    }
}
