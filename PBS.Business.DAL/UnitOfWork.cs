using PBS.Business.Contracts;
using PBS.Business.Contracts.Repositories;
using PBS.Business.DAL.Repositories;
using PBS.Database.Context;
using System;

namespace PBS.Business.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PbsDbContext _context;

        public IAuthRepository AuthRepository { get; }
        public IUserRepository UserRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public IParkingLotRepository ParkingLotRepository { get; }
        public ISlotRepository SlotRepository { get; }
        public ISlotTypeRepository SlotTypeRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IHomeRepository HomeRepository { get; }
        public IBookingRepository BookingRepository { get; }

        public UnitOfWork (PbsDbContext context)
        {
            _context = context;

            AuthRepository = new AuthRepository (_context);
            UserRepository = new UserRepository (_context);
            AddressRepository = new AddressRepository (_context);
            ParkingLotRepository = new ParkingLotRepository (_context);
            SlotRepository = new SlotRepository (_context);
            SlotTypeRepository = new SlotTypeRepository (_context);
            RoleRepository = new RoleRepository (_context);
            HomeRepository = new HomeRepository (_context);
            BookingRepository = new BookingRepository (_context);
        }

        public void SaveChanges ()
        {
            _context.SaveChanges ();
        }

        #region IDisposable implemention
        private bool disposed;

        protected virtual void Dispose (bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose ();
            }
            disposed = true;
        }

        public void Dispose ()
        {
            Dispose (true);
            GC.SuppressFinalize (this);
        }
        #endregion
    }
}
