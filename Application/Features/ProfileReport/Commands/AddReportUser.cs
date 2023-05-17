using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

using System.Net;

namespace Application.Features.Report.Commands
{
    public class AddReportUser
    {
        public class AddReportUserCommand : IRequest
        {
            public string Username { get; set; }
            public int ReasonId { get; set; }
            public string? Description { get; set; }
            public class AddReportUserHandler : IRequestHandler<AddReportUserCommand>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public AddReportUserHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<Unit> Handle(AddReportUserCommand request, CancellationToken cancellationToken)
                {

                    var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                    var target = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == request.Username);
                    var reason = await _unitOfWork.ReportReasons.GetQueryList().SingleOrDefaultAsync(x => x.Id == request.ReasonId);
                    if (target == null)
                        throw new RestException(HttpStatusCode.NotFound, "Not found");
                    if (reason == null)
                        throw new RestException(HttpStatusCode.NotFound, "Not found");
                    var reported = await _unitOfWork.ProfileReports.GetQueryList().SingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id && x.ReasonId == reason.Id);
                    if (reported != null)
                    {
                        reported.UpdateDate = DateTime.Now;
                        reported.Count = reported.Count + 1;
                        _unitOfWork.ProfileReports.Update(reported);
                    }
                    if (reported == null)
                    {
                        reported = new ProfileReport
                        {
                            Observer = observer,
                            Target = target,
                            Reason = reason,

                        };
                        reported.ReportDate = DateTime.Now;
                        reported.Count = 1;
                        reported.Description = request.Description;
                        _unitOfWork.ProfileReports.Insert(reported);
                    }
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return Unit.Value;
                    }
                    catch (Exception err)
                    {

                        throw new Exception("خطا در ذخیره اطلاعات!");

                    }
                }
            }
        }


    }
}
