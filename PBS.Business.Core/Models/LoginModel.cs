using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email Address is required field.")]
        [RegularExpression (@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
            ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required field.")]
        public string Password { get; set; }
    }
}
