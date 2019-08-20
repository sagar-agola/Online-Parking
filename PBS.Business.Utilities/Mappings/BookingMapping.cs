using AutoMapper;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;

namespace PBS.Business.Utilities.Mappings
{
    public class BookingMapping
    {
        private readonly IMapper _mapper;

        public BookingMapping (IMapper mapper)
        {
            _mapper = mapper;
        }

        public BookingViewModel MapBooking (Booking model)
        {
            BookingViewModel modelMapping = _mapper.Map<BookingViewModel> (model);



            return modelMapping;
        }
    }
}
