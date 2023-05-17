using Application.Dtos.Language;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Language.Queries
{
    public class GetAllLanguages : IRequest<IEnumerable<GetLanguageDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllLanguages(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllLanguagesHandler : IRequestHandler<GetAllLanguages, IEnumerable<GetLanguageDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllLanguagesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetLanguageDto>> Handle(GetAllLanguages query, CancellationToken cancellationToken)
            {

                var languageList = await _unitOfWork.Languages.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Icon)
                    .Select(c => new GetLanguageDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ShortName = c.ShortName,
                        Direction = (int)c.Direction,
                        IconId = c.IconId,
                        IconName = c.Icon.FileName,
                        CreationDate = c.CreationDate,
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .ToListAsync();
                if (languageList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                try
                {
                    var result = languageList.Adapt<IEnumerable<GetLanguageDto>>();
                    return result;
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی در گرفتن اطلاعات زبان رخ داد، این خطا مربوط به سرویس ارائه‌دهنده میباشد!");

                }
            }
        }
    }
}
