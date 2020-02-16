namespace ProfileAPI.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
