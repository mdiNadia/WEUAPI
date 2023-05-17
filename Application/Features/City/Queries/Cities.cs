using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.City.Queries
{
    public class Cities : IRequest<List<GetNameAndId>>
    {
        public List<int> ids { get; set; }
        public class CitiesHandler : IRequestHandler<Cities, List<GetNameAndId>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CitiesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndId>> Handle(Cities query, CancellationToken cancellationToken)
            {
                var Cities = await _unitOfWork.Cities
                    .GetQueryList().Where(c => query.ids.Contains(c.ProvinceId))
                    .AsNoTracking()
                    .Select(c => new GetNameAndId
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreationDate = c.CreationDate,
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .ToListAsync();
                if (Cities == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "هیچ شهری یافت نشد!");
                }
                return Cities;
            }
        }
    }
}
