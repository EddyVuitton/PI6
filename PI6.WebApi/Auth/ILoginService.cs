namespace PI6.WebApi.Auth;

public interface ILoginService
{
   public Task Login(string token);
   public Task Logout();
}