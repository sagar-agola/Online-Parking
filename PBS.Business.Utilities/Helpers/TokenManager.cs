using Microsoft.IdentityModel.Tokens;
using PBS.Business.Core.BusinessModels;
using PBS.Business.Utilities.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PBS.Business.Utilities.Helpers
{
    public class TokenManager : ITokenManager
    {
        private readonly IAppConfiguration _configuration;

        public TokenManager (IAppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken (UserViewModel model)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Name, model.FirstName + " " + model.LastName),
                new Claim(ClaimTypes.Role, model.RoleViewModel.Title)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey (Encoding.UTF8
                .GetBytes (_configuration.Token));

            SigningCredentials creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler ();

            SecurityToken token = tokenHandler.CreateToken (tokenDescriptor);

            return tokenHandler.WriteToken (token);
        }
    }
}
