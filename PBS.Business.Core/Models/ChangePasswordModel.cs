using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.Models
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Password is required field.")]
        [MinLength (6, ErrorMessage = "Password must be atleast 6 character long.")]
        [MaxLength (18, ErrorMessage = "Password must be atmost 18 character long.")]
        public string Password { get; set; }
    }
}
