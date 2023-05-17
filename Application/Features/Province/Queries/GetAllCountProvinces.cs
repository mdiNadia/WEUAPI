using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Province.Queries
{
    public class GetAllCountProvinces : IRequest<int>
    {
        public class GetAllCountProvincesHandler : IRequestHandler<GetAllCountProvinces, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountProvincesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountProvinces query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Provinces.GetQueryList()
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
