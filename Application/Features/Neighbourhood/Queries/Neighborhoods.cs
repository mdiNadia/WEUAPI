using Application.Dtos.Common;
using Application.Dtos.Lookup;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Neighbourhood.Queries
{
    public class Neighborhoods : IRequest<List<LookupDto>>
    {
        public List<int> Ids { get; set; }
        public class NeighborhoodsHandler : IRequestHandler<Neighborhoods, List<LookupDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public NeighborhoodsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<LookupDto>> Handle(Neighborhoods query, CancellationToken cancellationToken)
            {
                try
                {
                    var Neighborhoods = await _unitOfWork.Neighborhoods
                        .GetQueryList().Where(c => query.Ids.Contains(c.CityId))
                        .AsNoTracking()
                        .Select(c => new LookupDto
                        {
                            Id = c.Id,
                            Title = c.Name
                        })
                        .ToListAsync();
                    return Neighborhoods;
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }

            }
        }
    }
}
