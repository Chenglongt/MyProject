using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyProject.Api.Services
{
    public class TokenService
    {
        private IConfiguration _configuration;


        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwt(Guid userId, string userName, string address, string email) 
        {
            var secretKey = _configuration["Jwt:SecretKey"];

            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(secretKey!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var issuer = _configuration["Jwt:Issuer"];
            var expierInMinutes = Convert.ToInt32(_configuration["Jwt:ExpireInMinute"]);

            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.StreetAddress,address),
                new Claim(ClaimTypes.Email,email),
                ];


            var token = new JwtSecurityToken(
                issuer:issuer,
                audience: "*",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expierInMinutes),
                signingCredentials:credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
