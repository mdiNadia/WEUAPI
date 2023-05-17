using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Report.Commands
{
    public class AddReportAd
    {
        public class AddReportAdCommand : IRequest
        {
            public int ConfirmedResultId { get; set; }
            public int ReasonId { get; set; }
            public string? Description { get; set; }
            public class AddReportAdHandler : IRequestHandler<AddReportAdCommand>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public AddReportAdHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<Unit> Handle(AddReportAdCommand request, CancellationToken cancellationToken)
                {
                    var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                    var target = await _unitOfWork.ConfirmedResults.GetQueryList().SingleOrDefaultAsync(x => x.Id == request.ConfirmedResultId);
                    var reason = await _unitOfWork.ReportReasons.GetQueryList().SingleOrDefaultAsync(x => x.Id == request.ReasonId);
                    if (target == null)
                        throw new RestException(HttpStatusCode.NotFound, "Not found");
                    if (reason == null)
                        throw new RestException(HttpStatusCode.NotFound, "Not found");
                    var reported = await _unitOfWork.AdReports.GetQueryList().SingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id);
                    if (reported != null)
                    {
                        //null
                    }
                    if (reported == null)
                    {
                        reported = new AdReport
                        {
                            Observer = observer,
                            Target = target,
                            Reason = reason,

                        };
                        reported.Description = request.Description;
                        reported.CreationDate = DateTime.Now;
                        _unitOfWork.AdReports.Insert(reported);
                    }

                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return Unit.Value;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }



                }
            }
        }
    }
}
