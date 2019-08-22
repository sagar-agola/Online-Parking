using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class UpdatePasswordViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "New Password")]
        [Required (ErrorMessage = "Password is required field.")]
        [MinLength (6, ErrorMessage = "Password must be atleast 6 character long.")]
        [MaxLength (18, ErrorMessage = "Password must be atmost 18 character long.")]
        public string Password { get; set; }

        [Display (Name = "Confirm Password")]
        [Required (ErrorMessage = "Confirm password is required.")]
        [Compare ("Password", ErrorMessage = "Password and Confirm password did not match.")]
        public string ConfirmPassword { get; set; }

        [Required (ErrorMessage = "OTP is required.")]
        [StringLength (4, MinimumLength = 4, ErrorMessage = "OTP must be 4 digit long.")]
        [Range (0, int.MaxValue, ErrorMessage = "Enter valid OTP.")]
        public string OTP { get; set; }
    }
}
