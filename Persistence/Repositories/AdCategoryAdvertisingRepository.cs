using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class AdCategoryAdvertisingRepository : GenericRepository<AdCategoryAdvertising>, IAdCategoryAdvertisingRepository
    {
        public AdCategoryAdvertisingRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
