using PBS.Business.Core.BusinessModels;

namespace PBS.Business.Contracts.Services
{
    public interface IBookingService
    {
        BookingViewModel Add (BookingViewModel model);
        BookingViewModel Get (int id);
        System.Collections.Generic.List<BookingViewModel> GetAll ();
        System.Collections.Generic.List<BookingViewModel> GetByParkingLot (int parkingLotId);
        System.Collections.Generic.List<BookingViewModel> GetByUser (int userId);
        bool Update (BookingViewModel model);
    }
}
