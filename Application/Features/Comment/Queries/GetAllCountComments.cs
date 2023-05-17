using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Comment.Queries
{
    public class GetAllCountComments : IRequest<int>
    {
        public class GetAllCountCommentsHandler : IRequestHandler<GetAllCountComments, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountCommentsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountComments query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Comments.GetQueryList().AsNoTracking()
                                 .CountAsync();
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }

            }
        }
    }
}
