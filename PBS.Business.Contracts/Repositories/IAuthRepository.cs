using PBS.Database.Models;

namespace PBS.Business.Contracts.Repositories
{
    public interface IAuthRepository
    {
        User Register (User model);
        User Login (string email);
        bool EmailExists (string email);
    }
}
