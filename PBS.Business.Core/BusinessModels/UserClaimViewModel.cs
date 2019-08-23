using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.BusinessModels
{
    public class UserClaimViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Claim type is required.")]
        public string ClaimType { get; set; }

        [Required (ErrorMessage = "Claim title is required.")]
        public string ClaimTitle { get; set; }

        #region User claim
        public int UserId { get; set; }
        public UserViewModel UserViewModel { get; set; }
        #endregion
    }
}
