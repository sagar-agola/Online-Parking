using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBS.Database.Models
{
    public class Slot
    {
        public int Id { get; set; }

        public bool IsBooked { get; set; }

        [Column (TypeName = "text")]
        public string Description { get; set; }

        [Range (0, 99)]
        public int HourlyRate { get; set; }

        #region Slot parking lot
        public int ParkingLotId { get; set; }
        public ParkingLot ParkingLot { get; set; }
        #endregion

        #region Slot Type
        public int SlotTypeId { get; set; }
        public SlotType SlotType { get; set; }
        #endregion

        #region Navigational Properties
        public List<Booking> Bookings { get; set; }
        #endregion
    }
}
