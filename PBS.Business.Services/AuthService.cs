using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Utilities.Helpers;
using PBS.Business.Utilities.Mappings;
using PBS.Database.Models;

namespace PBS.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserMapping _userMapping;

        public AuthService (IUnitOfWork unitOfWork, IMapper mapper, IUserMapping userMapping)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userMapping = userMapping;
        }

        public UserViewModel Login (string email, string password)
        {
            User model = _unitOfWork.AuthRepository.Login (email);

            if (model == null)
            {
                return null;
            }

            if (!PasswordManager.VerifyPasswordHash (password, model.PasswordHash, model.PasswordSalt))
            {
                return null;
            }

            UserViewModel modelMapping = _userMapping.MapUser (model);

            return modelMapping;
        }

        public UserViewModel Register (UserViewModel model)
        {
            if (_unitOfWork.AuthRepository.EmailExists (model.Email))
            {
                return null;
            }

            model.RoleId = 2; // Role : User
            model.AddressId = 1; // initial Dummy address

            PasswordManager.CreatePasswordHash (model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User modelMapping = _mapper.Map<User> (model);

            modelMapping.PasswordHash = passwordHash;
            modelMapping.PasswordSalt = passwordSalt;

            modelMapping = _unitOfWork.AuthRepository.Register (modelMapping);
            _unitOfWork.SaveChanges ();

            model = _mapper.Map<UserViewModel> (modelMapping);

            return model;
        }
    }
}