using Microsoft.AspNetCore.Mvc;

namespace PBS.Web.Security
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute (string role = "NotRequired")
            : base (typeof (AuthorizationFilter))
        {
            Arguments = new object[] { role };
        }
    }
}
