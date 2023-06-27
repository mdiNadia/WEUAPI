using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Language.Queries
{
    public class GetAllLanguages : IRequest<IQueryable<GetLanguageDto>>
    {
        public GetAllLanguages()
        {
        }
        public class GetAllLanguagesHandler : IRequestHandler<GetAllLanguages, IQueryable<GetLanguageDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllLanguagesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetLanguageDto>> Handle(GetAllLanguages query, CancellationToken cancellationToken)
            {

                var languageList = _unitOfWork.Languages.GetQueryList()
                    .AsNoTracking()
                    .Select(c => new GetLanguageDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ShortName = c.ShortName,
                        Direction = (int)c.Direction,
                        IconId = c.IconId,
                        IconName = c.Icon.FileName,
                        CreationDate = c.CreationDate,
                    });
                return languageList;

            }
        }
    }
}
