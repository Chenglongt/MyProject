using System.Security.Cryptography;
using System.Text;

namespace MyProject.Api.Services
{
    public class PasswordService
    {
        private const int SaltSize = 18;
        public (string hashPassword, string salt) GenerateHashAndSalt(string plainPassword) {
        if(string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentNullException(nameof(plainPassword));
          var buffer =  RandomNumberGenerator.GetBytes(SaltSize);
          var salt = Convert.ToBase64String(buffer);

            var hashPassword = GenerateHashPassword(plainPassword, salt);
            return (hashPassword, salt);
           
        }

        public bool IsEqual(string plainPassword, string hashPassword, string salt) {
            var newHashPassword =  GenerateHashPassword(plainPassword, salt);
            return newHashPassword == hashPassword;
        }

        public static string GenerateHashPassword(string salt, string plainPassword) {
            var bytes = Encoding.UTF8.GetBytes(plainPassword + salt);
            var hash = SHA256.HashData(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
