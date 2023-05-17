using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.ReportReason.Commands
{
    public class DeleteReportReasonById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteReportReasonByIdHandler : IRequestHandler<DeleteReportReasonById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteReportReasonByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteReportReasonById command, CancellationToken cancellationToken)
            {

                var reportReason = await _unitOfWork.ReportReasons.GetByID(command.Id);
                if (reportReason == null) throw new RestException(HttpStatusCode.BadRequest, "دسته بندی وجود ندارد!");
                var CheckIfHasChildren = await _unitOfWork.ReportReasons.CheckIfHasChildren(command.Id);
                if (CheckIfHasChildren) throw new RestException(HttpStatusCode.BadRequest, "این دسته بندی دارای فرزند میباشد!");
                _unitOfWork.ReportReasons.Delete(reportReason);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{reportReason.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }





            }
        }
    }
}
