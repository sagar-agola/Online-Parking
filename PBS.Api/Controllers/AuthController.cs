using Microsoft.AspNetCore.Mvc;
using PBS.Business.Contracts.Services;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Core.Models;
using PBS.Business.Utilities.Helpers;

namespace PBS.Api.Controllers
{
    [Route ("api/auth")]
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

        [HttpPost ("login")]
        public object Login (LoginModel model)
        {
            UserViewModel response = _authService.Login (model.Email, model.Password);

            if (response == null)
            {
                return new ResponseDetails (false, null);
            }

            string token = _tokenManager.GetToken (response);

            return new ResponseDetails (true, token);
        }

        [HttpPost ("register")]
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

            string token = _tokenManager.GetToken (user);

            return new ResponseDetails (true, token);
        }
    }
}