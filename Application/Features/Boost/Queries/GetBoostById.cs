using Application.Dtos.Boost;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Province.Queries
{
    public class GetBoostById : IRequest<GetBoostDto>
    {
        public int Id { get; set; }
        public class GetBoostByIdHandler : IRequestHandler<GetBoostById, GetBoostDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetBoostByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetBoostDto> Handle(GetBoostById query, CancellationToken cancellationToken)
            {
                var city = await _unitOfWork.Boosts.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Advertising)
                    .Select(c => new GetBoostDto
                    {

                        Debit = c.Debit,
                        ValuePerVisit = c.ValuePerVisit,
                        NumberOfadViews = c.NumberOfadViews,
                        Advertising = new Dtos.Common.GetNameAndId
                        {
                            Id = c.AdvertisingId,
                            Name = c.Advertising.Name,
                        },
                        CreationDate = c.CreationDate
                    })
                    .FirstOrDefaultAsync();
                if (city == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return city;


            }
        }
    }
}
