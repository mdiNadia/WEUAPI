using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {

        public CommentRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<bool> CheckIfHasChildren(int commentId)
        {
            var result = await GetQueryList().AsNoTracking().AnyAsync(c => c.ParentId == commentId);
            return result;
        }
    }
}
