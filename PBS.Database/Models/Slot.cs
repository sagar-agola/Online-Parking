using System.Collections.Generic;

namespace PBS.Database.Models
{
    public class Slot
    {
        public int Id { get; set; }

        public bool IsBooked { get; set; }

        #region Slot parking lot
        public int ParkingLotId { get; set; }
        public ParkingLot ParkingLot { get; set; }
        #endregion

        #region Slot Type
        public int SlotId { get; set; }
        public SlotType SlotType { get; set; }
        #endregion

        #region Navigational Properties
        public List<Booking> Bookings { get; set; }
        #endregion
    }
}
