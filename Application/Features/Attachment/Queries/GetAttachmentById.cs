using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using System.Net;

namespace Application.Features.Attachment.Queries
{
    public class GetAttachmentById : IRequest<GetAttachmentDto>
    {
        public int Id { get; set; }
        public class GetAttachmentByIdHandler : IRequestHandler<GetAttachmentById, GetAttachmentDto>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetAttachmentByIdHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<GetAttachmentDto> Handle(GetAttachmentById query, CancellationToken cancellationToken)
            {

                var attachment = await _unitOfWork.Attachments.GetByID(query.Id);
                if (attachment == null) throw new RestException(HttpStatusCode.BadRequest, "فایل وجود ندارد!");

                var result = attachment.Adapt<GetAttachmentDto>();
                return result;


            }
        }
    }
}
