using PBS.Business.Contracts.Repositories;

namespace PBS.Business.Contracts
{
    public interface IUnitOfWork
    {
        IAuthRepository AuthRepository { get; }
        IUserRepository UserRepository { get; }
        IAddressRepository AddressRepository { get; }
        IParkingLotRepository ParkingLotRepository { get; }
        ISlotRepository SlotRepository { get; }
        ISlotTypeRepository SlotTypeRepository { get; }
        IRoleRepository RoleRepository { get; }
        IHomeRepository HomeRepository { get; }
        IBookingRepository BookingRepository { get; }

        void Dispose ();
        void SaveChanges ();
    }
}
