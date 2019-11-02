using PBS.Database.Models;

namespace PBS.Business.Contracts.Repositories
{
    public interface IBookingRepository
    {
        Booking Add (Booking model);
        bool BookingExists (int id);
        Booking Get (int id);
        System.Collections.Generic.List<Booking> GetAll ();
        System.Collections.Generic.List<Booking> GetByUser (int userId);
        void Update (Booking model);
    }
}
