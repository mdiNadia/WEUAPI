using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class AdCategoryCostRepository : GenericRepository<AdCategoryCost>, IAdCategoryCostRepository
    {
        public AdCategoryCostRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
