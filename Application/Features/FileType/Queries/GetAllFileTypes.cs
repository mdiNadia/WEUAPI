using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FileType.Queries
{
    public class GetAllFileTypes : IRequest<IQueryable<GetFileTypeDto>>
    {
        public GetAllFileTypes()
        {
        }

        public class GetAllFileTypesHandler : IRequestHandler<GetAllFileTypes, IQueryable<GetFileTypeDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllFileTypesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetFileTypeDto>> Handle(GetAllFileTypes query, CancellationToken cancellationToken)
            {

                var fileTypes = _unitOfWork.FileTypes.GetQueryList().AsNoTracking()
                    .Select(c => new GetFileTypeDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Size = c.Size,
                        Extension = c.Extension,
                        Type = c.Type,
                        CreationDate = c.CreationDate,
                    });

                return fileTypes;


            }
        }
    }
}
