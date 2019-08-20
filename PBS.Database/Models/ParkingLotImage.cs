namespace PBS.Database.Models
{
    public class ParkingLotImage
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        #region Parking Lot
        public int ParkingLotId { get; set; }
        public ParkingLot ParkingLot { get; set; }
        #endregion
    }
}
