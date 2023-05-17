using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Country.Queries
{
    public class GetAllCountCountries : IRequest<int>
    {
        public class GetAllCountCountriesHandler : IRequestHandler<GetAllCountCountries, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountCountriesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountCountries query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Countries.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");
                }
            }
        }
    }
}
