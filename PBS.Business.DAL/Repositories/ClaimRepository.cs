using Microsoft.EntityFrameworkCore;
using PBS.Business.Contracts.Repositories;
using PBS.Business.Core.Models;
using PBS.Database.Context;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.DAL.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly PbsDbContext _context;

        public ClaimRepository (PbsDbContext context)
        {
            _context = context;
        }

        public UserClaim Add (UserClaim model)
        {
            _context.Claims.Add (model);

            return model;
        }

        public List<UserClaim> GetAll ()
        {
            return _context.Claims
                .Include (claim => claim.User)
                .ToList ();
        }

        public List<UserClaim> GetUserClaims (int userId)
        {
            return _context.Claims
                .Include (claim => claim.User)
                .Where (claim => claim.UserId == userId)
                .ToList ();
        }

        public bool Remove (ClaimRemoveModel model)
        {
            UserClaim claim = _context.Claims.FirstOrDefault (c => c.UserId == model.UserId && c.ClaimType == model.ClaimTitle);

            if (claim == null)
            {
                return false;
            }

            _context.Claims.Remove (claim);
            return true;
        }

        public void RemoveAll (int userId)
        {
            List<UserClaim> claims = _context.Claims.Where (claim => claim.UserId == userId).ToList ();

            foreach (UserClaim claim in claims)
            {
                _context.Claims.Remove (claim);
            }
        }
    }
}
