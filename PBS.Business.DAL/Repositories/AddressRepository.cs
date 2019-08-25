using PBS.Business.Contracts.Repositories;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Linq;

namespace PBS.Business.DAL.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly PbsDbContext _context;

        public AddressRepository (PbsDbContext context)
        {
            _context = context;
        }

        public Address Add (Address model)
        {
            _context.Addresses.Add (model);

            return model;
        }

        public bool Update (Address model)
        {
            if (AddressExists (model.Id))
            {
                _context.Addresses.Update (model);

                return true;
            }

            return false;
        }

        public bool AddressExists (int id)
        {
            return _context.Addresses.Any (address => address.Id == id);
        }
    }
}
