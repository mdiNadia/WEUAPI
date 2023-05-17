using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class AdvertisingRepository : GenericRepository<Advertising>, IAdvertisingRepository
    {

        public AdvertisingRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
