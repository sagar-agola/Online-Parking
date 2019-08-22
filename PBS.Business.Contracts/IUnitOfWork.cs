using PBS.Business.Contracts.Repositories;

namespace PBS.Business.Contracts
{
    public interface IUnitOfWork
    {
        IAuthRepository AuthRepository { get; }
        IUserRepository UserRepository { get; }

        void Dispose ();
        void SaveChanges ();
    }
}
