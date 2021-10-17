using Core.Concrete;
using Core.Extensions;
using Core.Utility.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Security.JWT
{
    public class TokenHelper : ITokenHelper
    {
        private IConfiguration _configuration;
        private DateTime _accessTokenExpiration;
        private TokenOption _tokenOption;
       
        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOption = configuration.GetSection("TokenOption").Get<TokenOption>();
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var key = SecurityKeyHelper.CreateSecurityKey(_tokenOption.SecurityKey);
            var signInCredential = SigningCredentialHelper.CreateSigningCredentials(key);
            var jwt = CreateJwtSecurityToken(user, signInCredential, operationClaims);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Expiration = _accessTokenExpiration,
                Token = token
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(User user,SigningCredentials signingCredentials,List<OperationClaim> operationClaim)
        {
            var jwt = new JwtSecurityToken(
                issuer:_tokenOption.Issuer,
                audience:_tokenOption.Audience,
                expires:_accessTokenExpiration,
                notBefore:DateTime.Now,
                claims:SetClaim(user,operationClaim),
                signingCredentials:signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaim(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddEmail(user.Email);
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddName($"{user.FirstName}.{user.LastName}");
            claims.AddRoles(operationClaims.Select(s => s.Name).ToArray());
            return claims;
        }
    }
}
