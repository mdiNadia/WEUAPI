using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class ConfirmedResultRepository : GenericRepository<ConfirmResult>, IConfirmedResultRepository
    {
        public ConfirmedResultRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
