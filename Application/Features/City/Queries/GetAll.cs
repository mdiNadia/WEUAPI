using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.City.Queries
{
    public class GetAll : IRequest<List<GetNameAndId>>
    {

        public class GetAllHandler : IRequestHandler<GetAll, List<GetNameAndId>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndId>> Handle(GetAll query, CancellationToken cancellationToken)
            {
                var GetAll = await _unitOfWork.Cities
                    .GetQueryList()
                    .AsNoTracking()
                    .Select(c => new GetNameAndId
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreationDate = c.CreationDate,
                    })
                    .OrderByDescending(c => c.CreationDate).ToListAsync();
                if (GetAll == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "هیچ شهری یافت نشد!");
                }
                return GetAll;
            }
        }
    }
}
