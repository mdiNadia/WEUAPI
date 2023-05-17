using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AdCategoryRepository : GenericRepository<AdCategory>, IAdCategoryRepository
    {
        public AdCategoryRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<bool> CheckIfHasChildren(int AdCategoryId)
        {
            var result = await GetQueryList().AsNoTracking().AnyAsync(c => c.ParentId == AdCategoryId);
            return result;
        }

    }
}