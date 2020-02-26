using AutoMapper;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;

namespace PBS.Business.Utilities.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile ()
        {
            CreateMap<User, UserViewModel> ();
            CreateMap<UserViewModel, User> ();

            CreateMap<Role, RoleViewModel> ();
            CreateMap<RoleViewModel, Role> ();

            CreateMap<Slot, SlotViewModel> ();
            CreateMap<SlotViewModel, Slot> ();

            CreateMap<SlotType, SlotTypeViewModel> ();
            CreateMap<SlotTypeViewModel, SlotType> ();

            CreateMap<ParkingLot, ParkingLotViewModel> ();
            CreateMap<ParkingLotViewModel, ParkingLot> ();

            CreateMap<ParkingLotImage, ParkingLotImageViewModel> ();
            CreateMap<ParkingLotImageViewModel, ParkingLotImage> ();

            CreateMap<Booking, BookingViewModel> ();
            CreateMap<BookingViewModel, Booking> ();

            CreateMap<Address, AddressViewModel> ();
            CreateMap<AddressViewModel, Address> ();
        }
    }
}
