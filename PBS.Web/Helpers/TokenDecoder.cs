using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace PBS.Web.Helpers
{
    public class TokenDecoder : ITokenDecoder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenDecoder (IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string RowToken
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetString ("token");
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return RowToken != null;
            }
        }

        public int UserId
        {
            get
            {
                string token = RowToken;

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler ();
                JwtSecurityToken tokenS = handler.ReadToken (token) as JwtSecurityToken;

                return int.Parse (tokenS.Claims.First (claim => claim.Type == "nameid").Value);
            }
        }

        public string UserName
        {
            get
            {
                string token = RowToken;

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler ();
                JwtSecurityToken tokenS = handler.ReadToken (token) as JwtSecurityToken;

                return tokenS.Claims.First (claim => claim.Type == "unique_name").Value;
            }
        }

        public string UserRole
        {
            get
            {
                string token = RowToken;

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler ();
                JwtSecurityToken tokenS = handler.ReadToken (token) as JwtSecurityToken;

                return tokenS.Claims.First (claim => claim.Type == "role").Value;
            }
        }
    }
}
