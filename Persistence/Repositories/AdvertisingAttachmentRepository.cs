using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class AdvertisingAttachmentRepository : GenericRepository<AdvertisingAttachment>, IAdvertisingAttachmentRepository
    {
        public AdvertisingAttachmentRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
