using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Repositories
{
    public interface IParkingLotRepository
    {
        List<ParkingLot> GetAll ();
        List<ParkingLot> GetByUser (int userId);
        ParkingLot Get (int id);

        ParkingLot Add (ParkingLot model);
        void Update (ParkingLot model);
        void Remove (ParkingLot model);
        bool ParkingLotExists (int id);
    }
}
