namespace ProfileAPI.Data.Entities
{
    using System;

    public class ProfileVisitor
    {
        public Guid VisitorId { get; set; }
        public virtual UserProfile Visitor { get; set; }

        public Guid VisitedId { get; set; }
        public virtual UserProfile Visited { get; set; }

        public DateTime onDate { get; set; }
    }
}
