using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Business.Utilities.Mappings;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimMapping _claimMapping;

        public ClaimService (IUnitOfWork unitOfWork, IMapper mapper, IClaimMapping claimMapping)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimMapping = claimMapping;
        }

        public UserClaimViewModel Add (UserClaimViewModel model)
        {
            UserClaim modelMapping = _mapper.Map<UserClaim> (model);

            modelMapping = _unitOfWork.ClaimRepository.Add (modelMapping);
            _unitOfWork.SaveChanges ();

            return _mapper.Map<UserClaimViewModel> (modelMapping);
        }

        public List<UserClaimViewModel> GetAll ()
        {
            List<UserClaim> model = _unitOfWork.ClaimRepository.GetAll ();

            return _claimMapping.MapUserClaimList (model);
        }

        public List<UserClaimViewModel> GetUserClaim (int userId)
        {
            bool userExists = _unitOfWork.UserRepository.UserExists (userId);

            if (userExists)
            {
                List<UserClaim> model = _unitOfWork.ClaimRepository.GetUserClaims (userId);

                return _claimMapping.MapUserClaimList (model);
            }

            return null;
        }

        public bool Remove (ClaimRemoveModel model)
        {
            bool userExists = _unitOfWork.UserRepository.UserExists (model.UserId);

            if (userExists)
            {
                bool successed = _unitOfWork.ClaimRepository.Remove (model);

                if (successed)
                {
                    _unitOfWork.SaveChanges ();
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool RemoveAll (int userId)
        {
            bool userExists = _unitOfWork.UserRepository.UserExists (userId);

            if (userExists)
            {
                _unitOfWork.ClaimRepository.RemoveAll (userId);
                _unitOfWork.SaveChanges ();

                return true;
            }

            return false;
        }
    }
}
