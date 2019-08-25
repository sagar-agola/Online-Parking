using PBS.Database.Models;

namespace PBS.Business.Contracts.Repositories
{
    public interface IAddressRepository
    {
        Address Add (Address model);
        bool Update (Address model);
        bool AddressExists (int id);
    }
}
