using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
namespace Persistence.Repositories
{
    public class ProfileReportRepository : GenericRepository<ProfileReport>, IProfileReportRepository
    {

        public ProfileReportRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
