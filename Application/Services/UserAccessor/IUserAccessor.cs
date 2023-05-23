using Application.Dtos.Account;
using Application.Dtos.Register;
using Application.Services.JWT;
using Domain.Entities;

namespace Application.Services.UserAccessor
{
    public interface IUserAccessor
    {
        Task<RegisterResult> RegisterAsync(RegisterModel model);
        Task<bool> Verify(VerifyModel verifyModel);
        Task<bool> CheckVerifyCode(string code);
        Task<bool> CheckUsername(string username);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        ApplicationUser GetById(string id);
        Task<string> ForgetPassword(ForgotPasswordModel forgotPasswordModel);
        Task<object> VerifyResetPasswordToken(RequestUserResetModel requestUserResetModel);
        Task<string> ResetPassword(ResetPasswordModel resetPasswordModel);
        Task<string> GetCurrentUserIdAsync();
        string GetCurrentUserNameAsync();
        Task<ApplicationUser> GetUserByUsernameAsync(string username);


    }
}
