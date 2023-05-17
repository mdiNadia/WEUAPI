using Application.Errors;
using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Language.Queries
{
    public class GetAllCountLanguages : IRequest<int>
    {
        public class GetAllCountLanguagesHandler : IRequestHandler<GetAllCountLanguages, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountLanguagesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountLanguages query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Languages.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
