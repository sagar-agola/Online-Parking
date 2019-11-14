using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PBS.Web.Helpers;

namespace PBS.Web.Security
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly ITokenDecoder _tokenDecoder;
        private readonly string _role;

        public AuthorizationFilter (ITokenDecoder tokenDecoder, string role)
        {
            _tokenDecoder = tokenDecoder;
            _role = role;
        }

        public void OnAuthorization (AuthorizationFilterContext context)
        {
            if (!_tokenDecoder.IsLoggedIn)
            {
                context.Result = new RedirectResult ("LoginRequired");
            }
            else
            {
                if (_tokenDecoder.IsEmailConfirmed)
                {
                    if (_role != "NotRequired")
                    {
                        if (_tokenDecoder.UserRole != _role)
                        {
                            context.Result = new RedirectResult ("Unauthorized");
                        }
                    }
                }
                else
                {
                    context.Result = new RedirectResult ("EmailConfirmationRequired");
                }
            }
        }
    }
}
