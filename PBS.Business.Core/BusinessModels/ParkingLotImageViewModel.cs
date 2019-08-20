namespace PBS.Business.Core.BusinessModels
{
    public class ParkingLotImageViewModel
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        #region Parking Lot
        public int ParkingLotViewModelId { get; set; }
        public ParkingLotViewModel ParkingLotViewModel { get; set; }
        #endregion
    }
}
