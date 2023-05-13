using Microsoft.IdentityModel.Tokens;
using PI6.Shared.Data.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

    public static UserToken BuildToken(AccountDto accountDto, SymmetricSecurityKey symmetricSecurityKey)
    {
        var claims = new List<Claim>() {
            new Claim(ClaimTypes.Email, accountDto.UserEmail),
            new Claim(ClaimTypes.Role, accountDto.UstName)
        };

        var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddYears(1);

        JwtSecurityToken token = new(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}