﻿using System.Collections.Generic;

namespace PBS.Business.Core.BusinessModels
{
    public class SlotViewModel
    {
        public int Id { get; set; }
        public string EncryptedId { get; set; }

        public bool IsBooked { get; set; }

        #region Helper Properties
        public bool CanBook { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEnableBooking { get; set; }
        public bool CanDisableBooking { get; set; }
        public bool CanMakeAvailable { get; set; }
        public string Status { get; set; }
        #endregion

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
