using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Utilities.Mappings;
using PBS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRoleMapping _roleMapping;

        public RoleService (IUnitOfWork unitOfWork, IMapper mapper, IRoleMapping roleMapping)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleMapping = roleMapping;
        }

        public RoleViewModel Add (RoleViewModel model)
        {
            Role modelMapping = _mapper.Map<Role> (model);

            _unitOfWork.RoleRepository.Add (modelMapping);
            _unitOfWork.SaveChanges ();

            return Get (modelMapping.Id);
        }

        public RoleViewModel Get (int id)
        {
            if (_unitOfWork.RoleRepository.RoleExists (id))
            {
                Role model = _unitOfWork.RoleRepository.Get (id);

                return _roleMapping.MapRole (model);
            }

            return null;
        }

        public List<RoleViewModel> GetAll ()
        {
            List<Role> model = _unitOfWork.RoleRepository.GetAll ();

            return _roleMapping.MapRoleList (model);
        }

        public bool Remove (int id)
        {
            if (_unitOfWork.RoleRepository.RoleExists (id))
            {
                Role model = _unitOfWork.RoleRepository.Get (id);

                if (model.Users.Any (u => u.IsActive))
                {
                    return false;
                }

                _unitOfWork.RoleRepository.Remove (model);
                _unitOfWork.SaveChanges ();

                return true;
            }

            return false;
        }

        public bool Update (RoleViewModel model)
        {
            if (_unitOfWork.RoleRepository.RoleExists (model.Id))
            {
                Role modelMapping = _mapper.Map<Role> (model);

                _unitOfWork.RoleRepository.Update (modelMapping);
                _unitOfWork.SaveChanges ();

                return true;
            }

            return false;
        }
    }
}
