using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.AppSetting.Queries
{
    public class GetAppSetting : IRequest<GetAppsettingDto>
    {
        public class GetAppSettingHandler : IRequestHandler<GetAppSetting, GetAppsettingDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAppSettingHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetAppsettingDto> Handle(GetAppSetting query, CancellationToken cancellationToken)
            {
                var boostSetting = await _unitOfWork.AppSettings
                   .GetQueryList().AsNoTracking()
                   .Select(c => new GetAppsettingDto()
                   {
                       Id = c.Id,
                       CreationDate = c.CreationDate,
                       AppFee = c.AppFee,
                       Value = c.Value,
                       MinWeuPerVisit = c.MinValuePerVisit,
                       MinBoostAmount = c.MinBoostAmount,
                       MinView = c.MinView
                   }).FirstOrDefaultAsync();
                if (boostSetting == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return boostSetting;


            }
        }
    }
}
