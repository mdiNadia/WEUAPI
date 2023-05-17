using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class AdNeighborhoodRepository : GenericRepository<AdNeighborhood>, IAdNeighborhoodRepository
    {
        public AdNeighborhoodRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
