using Application.Dtos.Language;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ProfileScore.Queries
{
    public class GetProfileScoreById : IRequest<GetProfileScoreDto>
    {
        public int Id { get; set; }
        public class GetProfileScoreByIdHandler : IRequestHandler<GetProfileScoreById, GetProfileScoreDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetProfileScoreByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetProfileScoreDto> Handle(GetProfileScoreById query, CancellationToken cancellationToken)
            {

                var profileScore = await _unitOfWork.ProfileScores.GetQueryList()
                    .AsNoTracking()
                    .Where(x => x.Id == query.Id)
                    .Include(c => c.Icon)
                    .Select(c => new GetProfileScoreDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Score = c.Score,
                        IconId = c.IconId,
                        ProfileType = (int)c.ProfileType,
                        IconName = c.Icon.FileName,
                        CreationDate = c.CreationDate,
                    }).SingleOrDefaultAsync();
                if (profileScore == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return profileScore;


            }
        }
    }
}
