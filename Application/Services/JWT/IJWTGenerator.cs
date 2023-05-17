using Application.Dtos.Account;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Services.JWT
{
    public interface IJWTGenerator
    {
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
        Task<AuthenticationModel> RefreshTokenAsync(string token);
        bool RevokeToken(string token);
        RefreshToken CreateRefreshToken();
    }
}
