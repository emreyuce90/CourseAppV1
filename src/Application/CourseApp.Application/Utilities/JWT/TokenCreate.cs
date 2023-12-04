using CourseApp.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CourseApp.Application.Utilities.JWT {
    public class TokenCreate : ITokenCreate {
        private readonly IConfiguration _configuration;

        public TokenCreate(IConfiguration configuration) {
            _configuration = configuration;
        }

        public AccessToken CreateToken(User user) {
            var tokenExpireDate = DateTime.Now.AddHours(Convert.ToInt32(_configuration["Token:ExpireToken"]));
            var refreshTokenExpireDate = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Token:ExpireRefresh"]));
            SymmetricSecurityKey symmetric = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);

            //claims
            var claims = new List<Claim>() {
            new Claim("Id", $"{user.Id}"),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}")
            };

            //token create
            JwtSecurityToken securityToken = new(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: tokenExpireDate,
                notBefore: DateTime.Now,
                signingCredentials: credentials,
                claims: claims

                );
            //refresh token
           
            JwtSecurityTokenHandler tokenHandler = new();
            var accessToken = new AccessToken {
                ExpirationDate = tokenExpireDate,
                Token = tokenHandler.WriteToken(securityToken),
                RefreshToken = CreateRefreshToken(),
                RefreshTokenExpiration= refreshTokenExpireDate
            };
            return accessToken;
        }

        private string CreateRefreshToken() {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }
    }
}
