using System.Collections.Generic;

namespace PBS.Business.Core.BusinessModels
{
    public class SlotViewModel
    {
        public int Id { get; set; }

        public bool IsBooked { get; set; }

        #region Slot parking lot
        public int ParkingLotId { get; set; }
        public ParkingLotViewModel ParkingLotViewModel { get; set; }
        #endregion

        #region Slot Type
        public int SlotId { get; set; }
        public SlotTypeViewModel SlotTypeViewModel { get; set; }
        #endregion

        #region Navigational Properties
        public List<BookingViewModel> BookingViewModels { get; set; }
        #endregion
    }
}
