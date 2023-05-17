using Application.Errors;
using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Attachment.Queries
{
    public class GetAllCountAttachments : IRequest<int>
    {
        public class GetAllCountAttachmentsHandler : IRequestHandler<GetAllCountAttachments, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountAttachmentsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountAttachments query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Attachments.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
