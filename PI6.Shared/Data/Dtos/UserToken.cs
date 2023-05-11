namespace PI6.Shared.Data.Dtos;

public class UserToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}