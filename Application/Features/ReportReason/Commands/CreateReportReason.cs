using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Features.ReportReason.Commands
{
    public class CreateReportReason : IRequest<int>
    {
        public string Reason { get; set; }
        public int? ParentId { get; set; }
        public ReportReasonType ReasonType { get; set; }
        public class CreateReportReasonHandler : IRequestHandler<CreateReportReason, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateReportReasonHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateReportReason command, CancellationToken cancellationToken)
            {

                var reason = new Domain.Entities.ReportReason();
                switch (command.ReasonType)
                {
                    case ReportReasonType.user:
                        {
                            reason.ReportReasonType = ReportReasonType.user;
                            break;
                        }

                    case ReportReasonType.ad:
                        {
                            reason.ReportReasonType = ReportReasonType.ad;
                            break;
                        }
                }
                reason.CreationDate = DateTime.Now;
                reason.Reason = command.Reason;
                reason.ParentId = command.ParentId;

                _unitOfWork.ReportReasons.Insert(reason);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return reason.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }




            }
        }
    }
}
