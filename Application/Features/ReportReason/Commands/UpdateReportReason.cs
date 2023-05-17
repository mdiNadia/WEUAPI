using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.ReportReason.Commands
{
    public class UpdateReportReason : IRequest<int>
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int? ParentId { get; set; }

        public class UpdateReportReasonHandler : IRequestHandler<UpdateReportReason, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateReportReasonHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateReportReason command, CancellationToken cancellationToken)
            {

                var reportReason = await _unitOfWork.ReportReasons.GetByID(command.Id);

                if (reportReason == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                else
                {
                    reportReason.Reason = command.Reason;
                    reportReason.ParentId = command.ParentId;
                    _unitOfWork.ReportReasons.Update(reportReason);

                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return reportReason.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }
            }
        }
    }
}
