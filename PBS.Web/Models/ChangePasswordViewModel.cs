using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class ChangePasswordViewModel
    {
        [Required (ErrorMessage = "Email address is required.")]
        [EmailAddress (ErrorMessage = "Invalid Email address pattern.")]
        public string Email { get; set; }
    }
}
