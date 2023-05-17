using Application.Dtos.Language;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ProfileScore.Queries
{
    public class GetAllProfileScores : IRequest<IEnumerable<GetProfileScoreDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllProfileScores(IPaginationFilter filter)
        {
            _filter = filter;
        }

        public class GetAllProfileScoresHandler : IRequestHandler<GetAllProfileScores, IEnumerable<GetProfileScoreDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllProfileScoresHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetProfileScoreDto>> Handle(GetAllProfileScores query, CancellationToken cancellationToken)
            {

                var profileScoreList = await _unitOfWork.ProfileScores.GetQueryList().AsNoTracking().Include(c => c.Icon)
                    .Select(c => new GetProfileScoreDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Score = c.Score,
                        IconId = c.IconId,
                        ProfileType = (int)c.ProfileType,
                        IconName = c.Icon.FileName,
                        CreationDate = c.CreationDate,
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize).ToListAsync();
                if (profileScoreList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return profileScoreList;


            }
        }
    }
}
