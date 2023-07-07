using Application.Dtos.Common;
using Application.Dtos.Lookup;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.City.Queries
{
    public class Cities : IRequest<List<LookupDto>>
    {
        public List<int> ids { get; set; }
        public class CitiesHandler : IRequestHandler<Cities, List<LookupDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CitiesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<LookupDto>> Handle(Cities query, CancellationToken cancellationToken)
            {
                var Cities = await _unitOfWork.Cities
                    .GetQueryList().Where(c => query.ids.Contains(c.ProvinceId))
                    .AsNoTracking()
                    .Select(c => new LookupDto
                    {
                        Id = c.Id,
                        Title = c.Name,
                    })
                    .ToListAsync();
                return Cities;
            }
        }
    }
}
