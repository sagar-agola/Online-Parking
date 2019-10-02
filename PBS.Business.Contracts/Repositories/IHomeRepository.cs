using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Repositories
{
    public interface IHomeRepository
    {
        List<ParkingLot> Search (string query);
    }
}
