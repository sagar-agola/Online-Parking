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

        public UnitOfWork (PbsDbContext context)
        {
            _context = context;

            AuthRepository = new AuthRepository (_context);
        }

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

        public void SaveChanges ()
        {
            _context.SaveChanges ();
        }
    }
}
