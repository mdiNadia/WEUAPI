using Application.Features.Profile.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProfileRepository : IGenericRepository<Profile>
    {
        Task<ProfileDto> ReadProfile(string username, string currentUserName);
    }
}
