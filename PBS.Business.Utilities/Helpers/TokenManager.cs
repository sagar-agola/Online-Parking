using Microsoft.IdentityModel.Tokens;
using PBS.Business.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PBS.Business.Utilities.Helpers
{
    public class TokenManager : ITokenManager
    {
        private readonly IApiConfiguration _configuration;

        public TokenManager (IApiConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken (List<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey (Encoding.UTF8
                .GetBytes (_configuration.Token));

            SigningCredentials creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds,
                Issuer = _configuration.Issuer,
                Audience = _configuration.Audience
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler ();

            SecurityToken token = tokenHandler.CreateToken (tokenDescriptor);

            return tokenHandler.WriteToken (token);
        }
    }
}
