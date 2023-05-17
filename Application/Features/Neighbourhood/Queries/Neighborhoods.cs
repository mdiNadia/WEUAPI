using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Neighbourhood.Queries
{
    public class Neighborhoods : IRequest<List<GetNameAndId>>
    {
        public List<int> Ids { get; set; }
        public class NeighborhoodsHandler : IRequestHandler<Neighborhoods, List<GetNameAndId>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public NeighborhoodsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndId>> Handle(Neighborhoods query, CancellationToken cancellationToken)
            {
                try
                {
                    var Neighborhoods = await _unitOfWork.Neighborhoods
                        .GetQueryList().Where(c => query.Ids.Contains(c.CityId))
                        .AsNoTracking()
                        .Select(c => new GetNameAndId
                        {
                            Id = c.Id,
                            Name = c.Name,
                            CreationDate = c.CreationDate,
                        })
                        .OrderByDescending(c => c.CreationDate)
                        .ToListAsync();
                    if (Neighborhoods == null)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                    }
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
