using Identity.API.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.IdentityJWT
{
    public class TokenBuilder:ITokenBuilder
    {
        private readonly IConfiguration _configuration;

        public TokenBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public TokenResponse BuildToken(string username)
        {
           
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Auth:Secret")));

            var jwt = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Auth:Iss"),
                audience: _configuration.GetValue<string>("Auth:Audience"),
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(30)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new TokenResponse
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromDays(30).TotalDays
            };

            return responseJson;
        }
    }
}
