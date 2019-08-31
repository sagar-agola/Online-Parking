using System.Collections.Generic;

namespace PBS.Business.Core.BusinessModels
{
    public class ParkingLotViewModel
    {
        public int Id { get; set; }

        public bool IsAproved { get; set; }

        public bool IsActive { get; set; }

        #region Parking Owner
        public int OwnerId { get; set; }
        public UserViewModel OwnerViewModel { get; set; }
        #endregion

        #region Parking Address
        public int AddressId { get; set; }
        public AddressViewModel AddressViewModel { get; set; }
        #endregion

        #region Navigational Properties
        public List<ParkingLotImageViewModel> ParkingLotImageViewModels { get; set; }
        public List<SlotViewModel> SlotViewModels { get; set; }
        #endregion
    }
}
