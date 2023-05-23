using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.FileType.Queries
{
    public class GetAllFileTypes : IRequest<IEnumerable<GetFileTypeDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllFileTypes(IPaginationFilter filter)
        {
            _filter = filter;
        }

        public class GetAllFileTypesHandler : IRequestHandler<GetAllFileTypes, IEnumerable<GetFileTypeDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllFileTypesHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetFileTypeDto>> Handle(GetAllFileTypes query, CancellationToken cancellationToken)
            {

                var fileTypes = await _unitOfWork.FileTypes.GetQueryList()
                    .AsNoTracking()
                    .Select(c => new GetFileTypeDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Size = c.Size,
                        Extension = c.Extension,
                        Type = c.Type,
                        CreationDate = c.CreationDate,

                    })
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize).ToListAsync();
                if (fileTypes == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return fileTypes;


            }
        }
    }
}
