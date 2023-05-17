using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Province.Queries
{
    public class Provinces : IRequest<List<GetNameAndId>>
    {
        public int Id { get; set; }
        public class ProvincesHandler : IRequestHandler<Provinces, List<GetNameAndId>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ProvincesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndId>> Handle(Provinces query, CancellationToken cancellationToken)
            {

                var Provinces = await _unitOfWork.Provinces
                    .GetQueryList().Where(c => c.CountryId == query.Id)
                    .AsNoTracking()
                    .Select(c => new GetNameAndId
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreationDate = c.CreationDate
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .ToListAsync();
                if (Provinces == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                }
                return Provinces;


            }
        }
    }
}
