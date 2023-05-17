using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.FileType.Queries
{
    public class GetAllCountFileTypes : IRequest<int>
    {
        public class GetAllCountFileTypesHandler : IRequestHandler<GetAllCountFileTypes, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountFileTypesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountFileTypes query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.FileTypes.GetQueryList()
                        .AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
