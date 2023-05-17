using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class ApplicationRoleRepository : GenericRepository<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
