using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class ProfileBlocksRepository : GenericRepository<ProfileBlock>, IProfileBlocksRepository
    {

        public ProfileBlocksRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
