using PBS.Business.Core.BusinessModels;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Services
{
    public interface IHomeService
    {
        List<ParkingLotViewModel> Search (string query);
    }
}
