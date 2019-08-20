using System;
using System.Collections.Generic;
using System.Text;

namespace PBS.Database.Models
{
    public class ParkingLotImages
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        #region Parking Lot
        public int ParkingLotId { get; set; }
        public ParkingLot ParkingLot { get; set; }
        #endregion
    }
}
