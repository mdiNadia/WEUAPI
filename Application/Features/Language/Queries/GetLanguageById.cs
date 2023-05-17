using Application.Dtos.Language;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Language.Queries
{
    public class GetLanguageById : IRequest<GetLanguageDto>
    {
        public int Id { get; set; }
        public class GetLanguageByIdHandler : IRequestHandler<GetLanguageById, GetLanguageDto>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetLanguageByIdHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<GetLanguageDto> Handle(GetLanguageById query, CancellationToken cancellationToken)
            {

                var language = await _unitOfWork.Languages.GetQueryList()
                    .Include(c => c.Icon)
                    .Where(c => c.Id == query.Id).Select(c => new GetLanguageDto()
                    {
                        Id = query.Id,
                        Name = c.Name,
                        ShortName = c.ShortName,
                        Direction = (int)c.Direction,
                        IconId = c.IconId,
                        IconName = c.Icon.FileName,
                        CreationDate = c.CreationDate,
                    })
                   .FirstOrDefaultAsync();
                if (language == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return language;


            }
        }
    }
}
