using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
namespace Persistence.Repositories
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {

        public LanguageRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
