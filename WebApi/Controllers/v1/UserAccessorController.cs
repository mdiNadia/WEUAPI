using Application.Dtos.Account;
using Application.Dtos.Register;
using Application.Services.JWT;
using Application.Services.Sms;
using Application.Services.UserAccessor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using static System.Net.Mime.MediaTypeNames;
using ZXing.QrCode.Internal;

namespace WebApi.Controllers.v1
{
    public class UserAccessorController : BaseApiController
    {
        private readonly IUserAccessor _userAccessorRepository;
        private readonly IJWTGenerator _jWTGenerator;

        public UserAccessorController(IUserAccessor userAccessorRepository, IJWTGenerator jWTGenerator)
        {

            _userAccessorRepository = userAccessorRepository;
            this._jWTGenerator = jWTGenerator;
        }


        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {

            var result = await _userAccessorRepository.RegisterAsync(model);

            return Ok(result);
        }
        [HttpPost("CheckUsername")]
        public async Task<ActionResult> CheckUsername(string username)
        {
            var result = await _userAccessorRepository.CheckUsername(username);
            return Ok(result);
        }

        [HttpPost("Verify")]
        public async Task<ActionResult> Verify(VerifyModel model)
        {
            var result = await _userAccessorRepository.Verify(model);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await _userAccessorRepository.GetTokenAsync(model);
            SetRefreshTokenInCookie(result.RefreshToken);
            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userAccessorRepository.AddRoleAsync(model);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _jWTGenerator.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }


        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });
            var response = _jWTGenerator.RevokeToken(token);
            if (!response)
                return NotFound(new { message = "Token not found" });
            return Ok(new { message = "Token revoked" });
        }
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [Authorize(Roles = "User")]
        [HttpPost("tokens/{id}")]
        public IActionResult GetRefreshTokens(string id)
        {
            var user = _userAccessorRepository.GetById(id);
            return Ok(user.RefreshTokens);
        }

        [HttpPost("forgetpassword")]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var result = await _userAccessorRepository.ForgetPassword(forgotPasswordModel);
            return Ok(result);
        }

        [HttpPost("VerifyResetPasswordToken")]
        public async Task<IActionResult> VerifyResetPasswordToken([FromQuery] RequestUserResetModel requestUserResetModel)
        {
            var result = await _userAccessorRepository.VerifyResetPasswordToken(requestUserResetModel);
            return Ok(result);
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var result = await _userAccessorRepository.ResetPassword(resetPasswordModel);
            return Ok(result);
        }
    }
}
