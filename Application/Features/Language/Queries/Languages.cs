using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.AdCategory.Queries
{
    public class Languages : IRequest<List<GetNameAndIdString>>
    {

        public class LanguagesHandler : IRequestHandler<Languages, List<GetNameAndIdString>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public LanguagesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndIdString>> Handle(Languages query, CancellationToken cancellationToken)
            {

                var languages = await _unitOfWork.Languages
                    .GetQueryList()
                    .AsNoTracking()
                    .Select(c => new GetNameAndIdString()
                    {
                        Id = c.ShortName,
                        Name = c.Icon.FileName,
                        CreationDate = c.CreationDate,

                    })
                    .OrderByDescending(c => c.CreationDate)
                    .ToListAsync();
                if (languages == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return languages;


            }
        }
    }
}
