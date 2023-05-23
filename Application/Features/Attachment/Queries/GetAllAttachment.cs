using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Attachment.Queries
{
    public class GetAllAttachments : IRequest<IEnumerable<GetAttachmentDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllAttachments(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllAttachmentsHandler : IRequestHandler<GetAllAttachments, IEnumerable<GetAttachmentDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllAttachmentsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetAttachmentDto>> Handle(GetAllAttachments query, CancellationToken cancellationToken)
            {
                var attachmentList = await _unitOfWork.Attachments.GetQueryList().AsNoTracking()
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .ToListAsync();
                if (attachmentList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "فایل وجود ندارد!");

                }
                var result = attachmentList.Adapt<IEnumerable<GetAttachmentDto>>();
                return result;


            }
        }
    }
}
