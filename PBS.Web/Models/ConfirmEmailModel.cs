using System.ComponentModel.DataAnnotations;

namespace PBS.Web.Models
{
    public class ConfirmEmailModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [Required (ErrorMessage = "OTP is required.")]
        [StringLength (4, MinimumLength = 4, ErrorMessage = "OTP must be 4 digit long.")]
        [Range (1111, 9999, ErrorMessage = "Enter valid OTP.")]
        public string OTP { get; set; }
    }
}
