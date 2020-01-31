namespace WebGateway.Models.BidingModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserBindingModel
    {
        [Required(ErrorMessage = "Username required!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be min 2 and maximum 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email required!")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password required!")]
        public string ConfirmPassword { get; set; }
    }
}
