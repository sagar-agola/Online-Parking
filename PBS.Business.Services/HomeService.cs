using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Utilities.Helpers;
using PBS.Business.Utilities.Mappings;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.Services
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IParkingLotMapping _parkingLotMapping;

        public HomeService (IUnitOfWork unitOfWork, IParkingLotMapping parkingLotMapping)
        {
            _unitOfWork = unitOfWork;
            _parkingLotMapping = parkingLotMapping;
        }

        public List<ParkingLotViewModel> Search (string query)
        {
            string[] keywords = StringHelpers.FindWords (query);
            List<ParkingLotViewModel> parkingLots = new List<ParkingLotViewModel> ();

            if (keywords != null)
            {
                for(int i = 0; i < keywords.Length; i++)
                {
                    List<ParkingLot> model = _unitOfWork.HomeRepository.Search (keywords[i]);
                    List<ParkingLotViewModel> modelMapping = _parkingLotMapping.MapParkingLotList (model);

                    for (int j = 0; j < modelMapping.Count; j++)
                    {
                        if (!parkingLots.Any (x => x.Id == modelMapping[j].Id))
                        {
                            parkingLots.Add (modelMapping[j]);
                        }
                    }
                }
            }

            return parkingLots;
        }
    }
}
