using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace WebService.Services
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            using var sha256 = SHA256.Create();
            var providedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(providedPassword));

            return StructuralComparisons.StructuralEqualityComparer.Equals(hashedPasswordBytes, providedPasswordBytes);
        }
    }
}
