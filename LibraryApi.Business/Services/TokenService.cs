using LibraryApi.Business.Interfaces.Services;
using LibraryApi.Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Member member)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"]!;
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationInMinutes"]!);

            var claims = new List<Claim>
            {
                new Claim("MemberId",member.Id.ToString()),
                new Claim(ClaimTypes.Email,member.Email),
                new Claim(ClaimTypes.Role,member.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer : issuer,
                    audience: audience,
                    claims: claims,
                    expires : DateTime.UtcNow.AddMinutes(expirationMinutes),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
