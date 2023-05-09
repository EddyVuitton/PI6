using System.Security.Cryptography;
using System.Text;

namespace PI6.WebApi.Helpers;

public class AccountHelper
{
    //https://www.youtube.com/watch?v=ZbUCgU3G1z4&t=18s

    public static string HashPassword(string? password)
    {
        var passwordBytes = Encoding.Default.GetBytes(password ?? string.Empty);
        var hashedPassword = SHA256.HashData(passwordBytes);

        return Convert.ToHexString(hashedPassword);
    }
}