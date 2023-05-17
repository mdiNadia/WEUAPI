using WEUPanel.Pages.Account;

namespace WEUPanel.Services.Account
{
    public interface IAuthService
    {
        Task<AuthenticationModel> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}
