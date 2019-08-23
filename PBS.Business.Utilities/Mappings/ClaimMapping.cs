using AutoMapper;
using PBS.Business.Core.BusinessModels;
using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Utilities.Mappings
{
    public class ClaimMapping : IClaimMapping
    {
        private readonly IMapper _mapper;

        public ClaimMapping (IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserClaimViewModel MapUserClaim (UserClaim model)
        {
            UserClaimViewModel modelMapping = _mapper.Map<UserClaimViewModel> (model);

            if (model.User != null)
            {
                modelMapping.UserViewModel = _mapper.Map<UserViewModel> (model.User);
            }

            return modelMapping;
        }

        public List<UserClaimViewModel> MapUserClaimList (List<UserClaim> model)
        {
            List<UserClaimViewModel> modelMapping = new List<UserClaimViewModel> ();

            for (int i = 0; i < model.Count; i++)
            {
                modelMapping.Add (MapUserClaim (model[i]));
            }

            return modelMapping;
        }
    }
}
