using Application.Dtos.Common;
using Application.Dtos.Lookup;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Neighborhood.Queries
{
    public class GetAll : IRequest<IQueryable<LookupDto>>
    {

        public class GetAllHandler : IRequestHandler<GetAll, IQueryable<LookupDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<LookupDto>> Handle(GetAll query, CancellationToken cancellationToken)
            {
                var all = _unitOfWork.Neighborhoods
                    .GetQueryList()
                    .AsNoTracking()
                    .Select(c => new LookupDto
                    {
                        Id = c.Id,
                        Title = c.Name
                    });
                if (all == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                }
                return all;


            }
        }
    }
}
