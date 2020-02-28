namespace ProfileAPI.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        [Key]
        public long Id { get; set; }

        public string Url { get; set; }

        public DateTime UploadedOn { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
