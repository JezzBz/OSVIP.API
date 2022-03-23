using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Osvip.Api.Models;

namespace Osvip.Api.Data
{
    public class AuthOptions
    {
        public const string ISSUER = "OsvipApi"; // издатель токена
        public const string AUDIENCE = "OSVIPClIENT"; // потребитель токена
        private const string KEY = "7pHE}Se?ZbqkVQVJ#B8g2e~N6Y8k@4UO";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 день
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
        public static string HashPassword(string password, byte[] salt)
        {

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
        public static byte[] GenerateSault()
        {
            byte[] salt = new byte[128 / 8];
            Random random = new Random();
            random.NextBytes(salt);
            return salt;
        }
        public static ClaimsIdentity GetIdentity(User user)
        {

           
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,Enum.GetName(typeof(Roles),user.Role))
               
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
         
        }
        public static bool ValidetePassword(string password) => Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{6,32}$");
        public static bool ValidateEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }

        }
        public static IEnumerable<Claim> ReadJwtAccessToken(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(Token);
            var tokenS = jsonToken as JwtSecurityToken;
            return tokenS.Claims;
        }
    }
}

