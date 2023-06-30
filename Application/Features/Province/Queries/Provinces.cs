using Application.Dtos.Lookup;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Province.Queries
{
    public class Provinces : IRequest<List<LookupDto>>
    {
        public int Id { get; set; }
        public class ProvincesHandler : IRequestHandler<Provinces, List<LookupDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ProvincesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<LookupDto>> Handle(Provinces query, CancellationToken cancellationToken)
            {

                var Provinces = await _unitOfWork.Provinces
                    .GetQueryList().Where(c => c.CountryId == query.Id)
                    .AsNoTracking()
                    .Select(c => new LookupDto
                    {
                        Id = c.Id,
                        Title = c.Name,
                    })
                    .ToListAsync();
                return Provinces;
            }
        }
    }
}
