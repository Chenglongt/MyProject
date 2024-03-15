using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.IdentityModel.Tokens;
using MyProject.Shared.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyProject.Api.Services
{
    public class TokenService(IConfiguration configuration) 
    {
        private readonly IConfiguration _configuration = configuration;


        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration) =>new()
        {
            ValidateAudience = false,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =GetSecurityKey(configuration)
,
        };
        public string GenerateJwt(LogginUser user)
        {
            var securityKey = GetSecurityKey(_configuration);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var issuer = _configuration["Jwt:Issuer"];
            var expierInMinutes = Convert.ToInt32(_configuration["Jwt:ExpireInMinute"]);

            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Nmae),
                new Claim(ClaimTypes.StreetAddress,user.Address),
                new Claim(ClaimTypes.Email,user.Email),
                ];


            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: "*",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expierInMinutes),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private static SymmetricSecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secretKey = configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            return securityKey;
        }
    }
}
