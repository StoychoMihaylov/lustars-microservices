namespace WebGateway.Models.ViewModels
{
    public class Language
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual UserProfileViewModel UserProfile { get; set; }
    }
}
