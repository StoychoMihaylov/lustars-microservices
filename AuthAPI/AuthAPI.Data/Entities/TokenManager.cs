namespace AuthAPI.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TokenManager
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual User User { get; set; }
    }
}
