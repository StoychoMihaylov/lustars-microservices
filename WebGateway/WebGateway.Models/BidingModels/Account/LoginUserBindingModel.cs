namespace WebGateway.Models.BidingModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserBindingModel
    {
        [Required(ErrorMessage = "Email required!")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required!")]
        public string Password { get; set; }
    }
}
