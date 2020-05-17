namespace ProfileAPI.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Language
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
