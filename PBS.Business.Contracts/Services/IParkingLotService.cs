using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
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
        ParkingLotImageViewModel UploadImage (UploadLotImageModel model, string path);
        List<string> GetImages (int parkingLotId, string folderPath);
        List<ParkingLotViewModel> GetRequested ();
    }
}
