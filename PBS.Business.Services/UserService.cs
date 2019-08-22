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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserMapping _userMapping;

        public UserService (IUnitOfWork unitOfWork, IMapper mapper, IUserMapping userMapping)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userMapping = userMapping;
        }

        public List<UserViewModel> GetAll ()
        {
            List<User> model = _unitOfWork.UserRepository.GetAll ();

            if (model.Any ())
            {
                return _userMapping.MapUserList (model);
            }

            return null;
        }

        public UserViewModel Get (int id)
        {
            User model = _unitOfWork.UserRepository.Get (id);

            if (model == null)
            {
                return null;
            }

            return _userMapping.MapUser (model);
        }

        public UserViewModel Update (UserViewModel model)
        {
            User modelMapping = _mapper.Map<User> (model);

            bool success = _unitOfWork.UserRepository.Update (modelMapping);

            if (success)
            {
                _unitOfWork.SaveChanges ();
                return Get (model.Id);
            }

            return null;
        }
    }
}
