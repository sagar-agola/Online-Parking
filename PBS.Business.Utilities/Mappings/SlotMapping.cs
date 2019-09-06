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

            modelMapping.BookingViewModels = _mapper.Map<List<BookingViewModel>> (model.Bookings);
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
    }
}
