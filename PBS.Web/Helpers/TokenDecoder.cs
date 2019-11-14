using Microsoft.AspNetCore.Http;
using System;
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
                return Convert.ToInt32 (SecurityToken.Claims.First (claim => claim.Type == "nameid").Value);
            }
        }

        public string UserName
        {
            get
            {
                return SecurityToken.Claims.First (claim => claim.Type == "unique_name").Value;
            }
        }

        public string UserRole
        {
            get
            {
                return SecurityToken.Claims.First (claim => claim.Type == "role").Value;
            }
        }

        public bool IsEmailConfirmed
        {
            get
            {
                return Convert.ToBoolean(SecurityToken.Claims.First (claim => claim.Type == "IsEmailConfirmed").Value);
            }
        }

        private JwtSecurityToken SecurityToken
        {
            get
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler ();
                return handler.ReadToken (RowToken) as JwtSecurityToken;
            }
        }
    }
}
