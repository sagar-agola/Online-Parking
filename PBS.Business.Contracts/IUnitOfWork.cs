using PBS.Business.Contracts.Repositories;

namespace PBS.Business.Contracts
{
    public interface IUnitOfWork
    {
        IAuthRepository AuthRepository { get; }
        IUserRepository UserRepository { get; }
        IClaimRepository ClaimRepository { get; }

        void Dispose ();
        void SaveChanges ();
    }
}
