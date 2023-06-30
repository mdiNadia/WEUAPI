using Application.Dtos.Lookup;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Province.Queries
{
    public class GetAll : IRequest<IQueryable<LookupDto>>
    {
        public int Id { get; set; }
        public class GetAllHandler : IRequestHandler<GetAll, IQueryable<LookupDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<LookupDto>> Handle(GetAll query, CancellationToken cancellationToken)
            {

                var all = _unitOfWork.Provinces
                    .GetQueryList()
                    .AsNoTracking()
                    .Select(c => new LookupDto
                    {
                        Id = c.Id,
                        Title = c.Name,
                    });
                return all;


            }
        }
    }
}
