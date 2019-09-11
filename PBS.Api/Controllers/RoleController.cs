using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.ApiRoute;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;

namespace PBS.Api.Controllers
{
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController (IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpPost(ApiRoutes.Role.Add)]
        public object Add(RoleViewModel model)
        {
            model = _roleService.Add (model);

            if (model == null)
            {
                return new ResponseDetails (false, "Could not add new role.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpGet(ApiRoutes.Role.GetAll)]
        public object GetAll ()
        {
            List<RoleViewModel> model = _roleService.GetAll ();

            return new ResponseDetails (true, model);
        }

        [HttpGet(ApiRoutes.Role.Get)]
        public object Get(int id)
        {
            RoleViewModel model = _roleService.Get (id);

            if (model == null)
            {
                return new ResponseDetails (false, $"Role with Id : { id } not found.");
            }

            return new ResponseDetails (true, model);
        }

        [HttpPost(ApiRoutes.Role.Update)]
        public object Update(RoleViewModel model)
        {
            bool success = _roleService.Update (model);

            if (success)
            {
                return new ResponseDetails (true, "Role title updated successfully.");
            }

            return new ResponseDetails (false, $"Role with Id : { model.Id } not found.");
        }

        [HttpDelete(ApiRoutes.Role.Remove)]
        public object Remove(int id)
        {
            bool success = _roleService.Remove (id);

            if (success)
            {
                return new ResponseDetails (true, "Role removed successfully.");
            }

            return new ResponseDetails (false, $"Role with Id : { id } not found or remove existing user from this role.");
        }
    }
}