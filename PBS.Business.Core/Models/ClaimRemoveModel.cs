using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.Models
{
    public class ClaimRemoveModel
    {
        [Required (ErrorMessage = "User id is required field.")]
        public int UserId { get; set; }

        [Required (ErrorMessage = "Claim Title is required field.")]
        public string ClaimTitle { get; set; }
    }
}
