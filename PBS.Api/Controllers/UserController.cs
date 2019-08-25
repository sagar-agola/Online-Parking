﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using System.Collections.Generic;

namespace PBS.Api.Controllers
{
    [Route ("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet ("get-all")]
        [Authorize (Policy = "test")]
        public object GetAll ()
        {
            List<UserViewModel> model = _userService.GetAll ();

            if (model == null)
            {
                return new ResponseDetails (false, "None at the moment.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpGet ("get/{id}")]
        public object Get (int id)
        {
            UserViewModel model = _userService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"User with Id: { id } is not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpPost ("update")]
        public object Update (UserViewModel model)
        {
            int id = model.Id;
            model = _userService.Update (model);

            if (model == null)
            {
                return new ResponseDetails (false, $"User with Id: { id } is not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpPost ("change-password")]
        public object ChangePassword (ChangePasswordModel model)
        {
            bool success = _userService.ChangePassword (model);

            if (!success)
            {
                return new ResponseDetails (false, "Could not update password.");
            }

            return new ResponseDetails (true, "Password updated successfully.");
        }
    }
}
