using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Business.Core.BusinessModels
{
    public class ParkingLotViewModel
    {
        public int Id { get; set; }
        public string EncryptedId { get; set; }

        [Required (ErrorMessage = "Parking name is required.")]
        public string Name { get; set; }

        [Display (Name = "Is Aproved")]
        public bool IsAproved { get; set; }

        [Display (Name = "Is Active")]
        public bool IsActive { get; set; }

        #region Parking Owner
        public int OwnerId { get; set; }
        public string EncryptedOwnerId { get; set; }
        public UserViewModel OwnerViewModel { get; set; }
        #endregion

        #region Parking Address
        public int AddressId { get; set; }
        public string EncryptedAddressId { get; set; }
        public AddressViewModel AddressViewModel { get; set; }
        #endregion

        #region Navigational Properties
        public List<ParkingLotImageViewModel> ParkingLotImageViewModels { get; set; } = new List<ParkingLotImageViewModel> ();
        public List<SlotViewModel> SlotViewModels { get; set; } = new List<SlotViewModel> ();
        #endregion

        #region Helper Properties
        [Display (Name = "2 Wheeler Hourly Rate")]
        public int TwoWheelerHourlyRate { get; set; }

        [Display (Name = "4 Wheeler Hourly Rate")]
        public int FourWheelerHourlyRate { get; set; }
        #endregion
    }
}
