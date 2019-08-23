using System.ComponentModel.DataAnnotations;

namespace PBS.Database.Models
{
    public class UserClaim
    {
        public int Id { get; set; }

        [Required]
        public string ClaimType { get; set; }

        [Required]
        public string ClaimTitle { get; set; }

        #region User claim
        public int UserId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
