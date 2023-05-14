namespace PI6.WebApi.Auth;

public interface ILoginService
{
   public Task Login(string token, string email);
   public Task Logout();
}