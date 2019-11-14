using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.BusinessModels
{
    public class AddressViewModel
    {
        public int? Id { get; set; }
        public string EncryptedId { get; set; }

        [Required (ErrorMessage = "Address line 1 is required.")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display (Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        public string LandMark { get; set; }

        [Required]
        public string City { get; set; }

        [Required (ErrorMessage = "Pincode is required.")]
        [MinLength (6)]
        [MaxLength (6)]
        [Display(Name = "Pincode")]
        public string PinCode { get; set; }

        [Required(ErrorMessage = "Sub District is required.")]
        [Display(Name = "Sub District")]
        public string SubDistrict { get; set; }

        [Required (ErrorMessage = "District is required.")]
        public string District { get; set; }

        [Required (ErrorMessage = "State is required.")]
        public string State { get; set; }

        #region Navigational Properties
        public List<UserViewModel> UserViewModels { get; set; }

        public List<ParkingLotViewModel> ParkingLotViewModels { get; set; }
        #endregion
    }
}
