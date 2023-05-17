using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
namespace Persistence.Repositories
{
    public class AdReportRepository : GenericRepository<AdReport>, IAdReportRepository
    {

        public AdReportRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
