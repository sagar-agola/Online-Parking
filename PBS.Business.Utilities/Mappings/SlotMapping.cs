using AutoMapper;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Utilities.Mappings
{
    public class SlotMapping : ISlotMapping
    {
        private readonly IMapper _mapper;

        public SlotMapping (IMapper mapper)
        {
            _mapper = mapper;
        }

        public SlotViewModel MapSlot (Slot model)
        {
            SlotViewModel modelMapping = _mapper.Map<SlotViewModel> (model);

            modelMapping.BookingViewModels = MapBookings (model.Bookings);
            modelMapping.SlotTypeViewModel = _mapper.Map<SlotTypeViewModel> (model.SlotType);
            modelMapping.ParkingLotViewModel = _mapper.Map<ParkingLotViewModel> (model.ParkingLot);

            return modelMapping;
        }

        public List<SlotViewModel> MapSlotList (List<Slot> model)
        {
            List<SlotViewModel> modelMapping = new List<SlotViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                modelMapping.Add (MapSlot (model[i]));
            }

            return modelMapping;
        }

        private List<BookingViewModel> MapBookings (List<Booking> model)
        {
            List<BookingViewModel> modelMapping = new List<BookingViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                BookingViewModel booking = _mapper.Map<BookingViewModel> (model[i]);

                booking.CustomerViewModel = _mapper.Map<UserViewModel> (model[i].Customer);

                modelMapping.Add (booking);
            }

            return modelMapping;
        }
    }
}
