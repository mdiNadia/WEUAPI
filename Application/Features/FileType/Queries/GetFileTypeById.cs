using Application.Dtos.FileType;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.FileType.Queries
{
    public class GetFileTypeById : IRequest<GetFileTypeDto>
    {
        public int Id { get; set; }
        public class GetFileTypeByIdHandler : IRequestHandler<GetFileTypeById, GetFileTypeDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetFileTypeByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetFileTypeDto> Handle(GetFileTypeById query, CancellationToken cancellationToken)
            {
                var fileType = await _unitOfWork.FileTypes.GetQueryList()
                    .Where(c => c.Id == query.Id)
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
                    .FirstOrDefaultAsync();
                if (fileType == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return fileType;


            }
        }
    }
}
