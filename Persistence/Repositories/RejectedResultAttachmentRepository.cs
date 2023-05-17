using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class RejectedResultAttachmentRepository : GenericRepository<RejectedResultAttachment>, IRejectedResultAttachmentRepository
    {
        public RejectedResultAttachmentRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
