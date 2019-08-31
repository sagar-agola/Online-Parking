using System.Collections.Generic;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;

namespace PBS.Business.Utilities.Mappings
{
    public interface IParkingLotMapping
    {
        ParkingLotViewModel MapParkingLot (ParkingLot model);
        List<ParkingLotViewModel> MapParkingLotList (List<ParkingLot> model);
    }
}