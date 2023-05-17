

using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class ProfileScoreRepository : GenericRepository<ProfileScore>, IProfileScoreRepository
    {


        public ProfileScoreRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}