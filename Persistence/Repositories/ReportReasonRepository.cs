using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ReportReasonRepository : GenericRepository<ReportReason>, IReportReasonRepository
    {

        public ReportReasonRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<bool> CheckIfHasChildren(int reasonId)
        {
            var result = await GetQueryList().AsNoTracking().AnyAsync(c => c.ParentId == reasonId);
            return result;
        }
    }
}
