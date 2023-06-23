using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ProfileScore.Queries
{
    public class GetAllProfileScores : IRequest<IQueryable<GetProfileScoreDto>>
    {
        public GetAllProfileScores()
        {
        }

        public class GetAllProfileScoresHandler : IRequestHandler<GetAllProfileScores, IQueryable<GetProfileScoreDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllProfileScoresHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetProfileScoreDto>> Handle(GetAllProfileScores query, CancellationToken cancellationToken)
            {

                var profileScoreList = _unitOfWork.ProfileScores.GetQueryList().AsNoTracking().Include(c => c.Icon)
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
                    .OrderByDescending(c => c.CreationDate);
                
                return profileScoreList;
            }
        }
    }
}
