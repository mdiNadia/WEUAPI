using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class TransactionTypeRepository : GenericRepository<Domain.Entities.TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
