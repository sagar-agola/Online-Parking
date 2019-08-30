using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.ApiRoute;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Business.Utilities.Helpers;
using System.Collections.Generic;
using System.Security.Claims;

namespace PBS.Api.Controllers
{

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenManager _tokenManager;

        public AuthController (IAuthService authService, ITokenManager tokenManager)
        {
            _authService = authService;
            _tokenManager = tokenManager;
        }

        [HttpPost (ApiRoutes.Auth.Login)]
        public object Login (LoginModel model)
        {
            UserViewModel response = _authService.Login (model.Email, model.Password);

            if (response == null)
            {
                return new ResponseDetails (false, null);
            }

            List<Claim> claims = GetClaims (response);

            string token = _tokenManager.GetToken (claims);

            return new ResponseDetails (true, token);
        }

        [HttpPost (ApiRoutes.Auth.Register)]
        public object Register (UserViewModel model)
        {
            string password = model.Password;

            model = _authService.Register (model);

            if (model == null)
            {
                return new ResponseDetails (false, "Email already exists.");
            }

            // login after register
            UserViewModel user = _authService.Login (model.Email, password);

            List<Claim> claims = GetClaims (user);

            string token = _tokenManager.GetToken (claims);

            return new ResponseDetails (true, token);
        }

        private static List<Claim> GetClaims (UserViewModel model)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, model.Id.ToString ()),
                new Claim (ClaimTypes.Name, model.FirstName + " " + model.LastName),
                new Claim (ClaimTypes.Role, model.RoleViewModel.Title)
            };

            foreach (UserClaimViewModel claim in model.UserClaimViewModels)
            {
                claims.Add (new Claim (claim.ClaimType, claim.ClaimTitle));
            }

            return claims;
        }
    }
}