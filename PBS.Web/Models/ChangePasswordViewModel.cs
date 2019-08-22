using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Email address is required.")]
        [EmailAddress (ErrorMessage = "Invalid Email address pattern.")]
        public string Email { get; set; }
    }
}
