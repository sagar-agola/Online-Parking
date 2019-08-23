using AutoMapper;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Utilities.Mappings
{
    public class UserMapping : IUserMapping
    {
        private readonly IMapper _mapper;

        public UserMapping (IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Public Methods
        public UserViewModel MapUser (User model)
        {
            UserViewModel modelMapping = _mapper.Map<UserViewModel> (model);

            modelMapping.RoleViewModel = _mapper.Map<RoleViewModel> (model.Role);
            modelMapping.AddressViewModel = _mapper.Map<AddressViewModel> (model.Address);
            modelMapping.UserClaimViewModels = _mapper.Map<List<UserClaimViewModel>> (model.UserClaims);
            modelMapping.BookingViewModels = MapBookings (model.Bookings);
            modelMapping.ParkingLotViewModels = MapParkingLots (model.ParkingLots);

            return modelMapping;
        }

        public List<UserViewModel> MapUserList (List<User> model)
        {
            List<UserViewModel> modelMapping = new List<UserViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                modelMapping.Add (MapUser (model[i]));
            }

            return modelMapping;
        }
        #endregion

        #region Private helping methods
        private List<ParkingLotViewModel> MapParkingLots (List<ParkingLot> model)
        {
            List<ParkingLotViewModel> modelMapping = new List<ParkingLotViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                ParkingLotViewModel viewModel = _mapper.Map<ParkingLotViewModel> (model[i]);
                viewModel.AddressViewModel = _mapper.Map<AddressViewModel> (model[i].Address);

                modelMapping.Add (viewModel);
            }

            return modelMapping;
        }

        private List<BookingViewModel> MapBookings (List<Booking> model)
        {
            List<BookingViewModel> modelMapping = new List<BookingViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                BookingViewModel viewModel = _mapper.Map<BookingViewModel> (model[i]);
                viewModel.SlotViewModel = _mapper.Map<SlotViewModel> (model[i].Slot);

                modelMapping.Add (viewModel);
            }

            return modelMapping;
        }
        #endregion
    }
}
