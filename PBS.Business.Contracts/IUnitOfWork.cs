using PBS.Business.Contracts.Repositories;

namespace PBS.Business.Contracts
{
    public interface IUnitOfWork
    {
        IAuthRepository AuthRepository { get; }
        IUserRepository UserRepository { get; }
        IClaimRepository ClaimRepository { get; }
        IAddressRepository AddressRepository { get; }
        IParkingLotRepository ParkingLotRepository { get; }
        ISlotRepository SlotRepository { get; }
        ISlotTypeRepository SlotTypeRepository { get; }

        void Dispose ();
        void SaveChanges ();
    }
}
