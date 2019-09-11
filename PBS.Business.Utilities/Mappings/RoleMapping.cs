using AutoMapper;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Utilities.Mappings
{
    public class RoleMapping : IRoleMapping
    {
        private readonly IMapper _mapper;

        public RoleMapping (IMapper mapper)
        {
            _mapper = mapper;
        }

        public RoleViewModel MapRole (Role model)
        {
            RoleViewModel modelMapping = _mapper.Map<RoleViewModel> (model);

            modelMapping.UserViewModels = _mapper.Map<List<UserViewModel>> (model.Users);

            return modelMapping;
        }

        public List<RoleViewModel> MapRoleList (List<Role> model)
        {
            List<RoleViewModel> modelMapping = new List<RoleViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                modelMapping.Add (MapRole (model[i]));
            }

            return modelMapping;
        }
    }
}
