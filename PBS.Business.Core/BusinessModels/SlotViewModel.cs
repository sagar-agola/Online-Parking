using System;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.Core.BusinessModels
{
    public class SlotViewModel
    {
        public int Id { get; set; }
        public string EncryptedId { get; set; }

        public bool IsBooked { get; set; }

        public bool CanBook { get; set; }

        #region Slot parking lot
        public int ParkingLotId { get; set; }
        public string EncryptedParkingLotId { get; set; }
        public ParkingLotViewModel ParkingLotViewModel { get; set; }
        #endregion

        #region Slot Type
        public int SlotTypeId { get; set; }
        public string EncryptedSlotTypeId { get; set; }
        public SlotTypeViewModel SlotTypeViewModel { get; set; }
        #endregion

        #region Navigational Properties
        public List<BookingViewModel> BookingViewModels { get; set; }
        #endregion
    }
}
