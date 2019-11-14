using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.BusinessModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string EncryptedId { get; set; }

        [Required (ErrorMessage = "Firstname is required field.")]
        [StringLength (50, ErrorMessage = "Maximum 50 character allowed for FirstName")]
        public string FirstName { get; set; }

        [StringLength (50, ErrorMessage = "Maximum 50 character allowed for LastName")]
        public string LastName { get; set; }

        [Required (ErrorMessage = "Email address is required field")]
        [RegularExpression (@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
            ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }

        [StringLength (10, MinimumLength = 10, ErrorMessage = "Contect number must be 10 character long.")]
        public string ContectNumber { get; set; }

        [Required (ErrorMessage = "Password is required field.")]
        [MinLength (6, ErrorMessage = "Password must be atleast 6 character long.")]
        [MaxLength (18, ErrorMessage = "Password must be atmost 18 character long.")]
        public string Password { get; set; }

        [Required (ErrorMessage = "Confirm Password is required field.")]
        [Compare("Password", ErrorMessage = "Password and Compare password must match.")]
        public string ConfirmPassword { get; set; }

        public bool IsActive { get; set; }

        public bool IsEmailConfirmed { get; set; }

        #region User Role
        public int RoleId { get; set; }
        public string EncryptedRoleId { get; set; }
        public RoleViewModel RoleViewModel { get; set; }
        #endregion

        #region User Address
        public int AddressId { get; set; }
        public AddressViewModel AddressViewModel { get; set; }
        #endregion

        #region Navigational Properties
        public List<ParkingLotViewModel> ParkingLotViewModels { get; set; }
        public List<BookingViewModel> BookingViewModels { get; set; }
        public List<UserClaimViewModel> UserClaimViewModels { get; set; }
        #endregion
    }
}
