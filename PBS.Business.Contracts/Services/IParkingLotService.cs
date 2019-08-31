using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Services
{
    public interface IParkingLotService
    {
        List<ParkingLotViewModel> GetAll ();
        List<ParkingLotViewModel> GetByUser (int userId);
        ParkingLotViewModel Get (int id);

        ParkingLotViewModel Add (ParkingLotViewModel model);
        ParkingLotViewModel Update (ParkingLotViewModel model);
        bool Remove (int id);
    }
}
