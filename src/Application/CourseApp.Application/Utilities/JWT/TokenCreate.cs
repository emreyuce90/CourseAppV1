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

        public AccessToken CreateToken(User user, int hours) {
            var expireDate = DateTime.Now.AddHours(hours);
            SymmetricSecurityKey symmetric = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);

            //claims
            var claims = new List<Claim>() {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}")
            };

            //token create
            JwtSecurityToken securityToken = new(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: DateTime.Now.AddMinutes(hours),
                notBefore: DateTime.Now,
                signingCredentials: credentials,
                claims: claims

                );
            //refresh token
            byte[] numbers = new byte[32];
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(numbers);
            var refreshToken = Convert.ToBase64String(numbers);
            JwtSecurityTokenHandler tokenHandler = new();
            var accessToken = new AccessToken {
                ExpirationDate = expireDate,
                Token = tokenHandler.WriteToken(securityToken),
                RefreshToken = refreshToken
            };
            return accessToken;
        }


    }
}
