using AutoMapper;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Utilities.Mappings
{
    public class BookingMapping : IBookingMapping
    {
        private readonly IMapper _mapper;

        public BookingMapping (IMapper mapper)
        {
            _mapper = mapper;
        }

        public BookingViewModel MapBooking (Booking model)
        {
            BookingViewModel modelMapping = _mapper.Map<BookingViewModel> (model);

            modelMapping.CustomerViewModel = _mapper.Map<UserViewModel> (model.Customer);
            modelMapping.SlotViewModel = MapSlot (model.Slot);

            return modelMapping;
        }

        public List<BookingViewModel> MapBookingList (List<Booking> model)
        {
            List<BookingViewModel> modelMapping = new List<BookingViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                modelMapping.Add (MapBooking (model[i]));
            }

            return modelMapping;
        }

        private SlotViewModel MapSlot (Slot model)
        {
            SlotViewModel modelMapping = _mapper.Map<SlotViewModel> (model);

            modelMapping.SlotTypeViewModel = _mapper.Map<SlotTypeViewModel> (model.SlotType);
            modelMapping.ParkingLotViewModel = _mapper.Map<ParkingLotViewModel> (model.ParkingLot);

            return modelMapping;
        }
    }
}
