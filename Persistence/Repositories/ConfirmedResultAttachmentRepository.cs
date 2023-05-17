using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class ConfirmedResultAttachmentRepository : GenericRepository<ConfirmedResultAttachment>, IConfirmedResultAttachmentRepository
    {
        public ConfirmedResultAttachmentRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
