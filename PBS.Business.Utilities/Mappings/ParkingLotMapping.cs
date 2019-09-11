using AutoMapper;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Utilities.Mappings
{
    public class ParkingLotMapping : IParkingLotMapping
    {
        private readonly IMapper _mapper;

        public ParkingLotMapping (IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Public Methods
        public ParkingLotViewModel MapParkingLot (ParkingLot model)
        {
            ParkingLotViewModel modelMapping = _mapper.Map<ParkingLotViewModel> (model);

            modelMapping.AddressViewModel = _mapper.Map<AddressViewModel> (model.Address);
            modelMapping.OwnerViewModel = MapUser (model.Owner);
            modelMapping.ParkingLotImageViewModels = _mapper.Map<List<ParkingLotImageViewModel>> (model.ParkingLotImages);
            modelMapping.SlotViewModels = MapSlots (model.Slots);

            return modelMapping;
        }

        public List<ParkingLotViewModel> MapParkingLotList (List<ParkingLot> model)
        {
            List<ParkingLotViewModel> modelMapping = new List<ParkingLotViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                modelMapping.Add (MapParkingLot (model[i]));
            }

            return modelMapping;
        }
        #endregion

        #region Private Methods
        private UserViewModel MapUser(User model)
        {
            UserViewModel modelMapping = _mapper.Map<UserViewModel> (model);

            modelMapping.RoleViewModel = _mapper.Map<RoleViewModel> (model.Role);
            modelMapping.AddressViewModel = _mapper.Map<AddressViewModel> (model.Address);

            return modelMapping;
        }

        private List<SlotViewModel> MapSlots (List<Slot> model)
        {
            List<SlotViewModel> modelMapping = new List<SlotViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                SlotViewModel slot = _mapper.Map<SlotViewModel> (model[i]);
                slot.SlotTypeViewModel = _mapper.Map<SlotTypeViewModel> (model[i].SlotType);
                slot.BookingViewModels = _mapper.Map<List<BookingViewModel>> (model[i].Bookings);

                modelMapping.Add (slot);
            }

            return modelMapping;
        }
        #endregion
    }
}
