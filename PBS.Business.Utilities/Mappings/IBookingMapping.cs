using System.Collections.Generic;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;

namespace PBS.Business.Utilities.Mappings
{
    public interface IBookingMapping
    {
        BookingViewModel MapBooking (Booking model);
        List<BookingViewModel> MapBookingList (List<Booking> model);
    }
}