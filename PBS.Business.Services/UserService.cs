﻿using AutoMapper;
using PBS.Business.Contracts;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Business.Utilities.Helpers;
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

            return new List<UserViewModel>();
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

        public bool Remove (int id)
        {
            if (_unitOfWork.UserRepository.UserExists (id))
            {
                _unitOfWork.UserRepository.Remove (id);
                _unitOfWork.SaveChanges ();

                return true;
            }

            return false;
        }

        public bool ChangePassword (ChangePasswordModel model)
        {
            PasswordManager.CreatePasswordHash (model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            ChangePasswordDbModel dbModel = new ChangePasswordDbModel
            {
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            if (_unitOfWork.AuthRepository.EmailExists (model.Email))
            {
                _unitOfWork.UserRepository.ChangePassword (dbModel);
                _unitOfWork.SaveChanges ();

                return true;
            }

            return false;
        }

        public bool ChangeRole (ChangeUserRoleModel model)
        {
            bool roleExists = _unitOfWork.RoleRepository.RoleExists (model.RoleId);

            if (roleExists)
            {
                User user = _unitOfWork.UserRepository.Get (model.UserId);

                if (user != null)
                {
                    user.RoleId = model.RoleId;

                    _unitOfWork.UserRepository.Update (user);
                    _unitOfWork.SaveChanges ();

                    return true;
                }
            }

            return false;
        }

        public bool MakeOwner (int userId)
        {
            bool success = _unitOfWork.UserRepository.MakeOwner (userId);
            _unitOfWork.SaveChanges ();

            return success;
        }

        public bool EmailExists(string email)
        {
            return _unitOfWork.AuthRepository.EmailExists (email);
        }
    }
}
