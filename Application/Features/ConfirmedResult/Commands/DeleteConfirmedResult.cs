using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;

namespace Application.Features.ConfirmedResult.Commands
{
    public class DeleteConfirmedResultById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteConfirmedResultByIdHandler : IRequestHandler<DeleteConfirmedResultById, string>
        {
            private readonly IMediator _mediator;
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteConfirmedResultByIdHandler(IMediator mediator, IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteConfirmedResultById command, CancellationToken cancellationToken)
            {

                var ConfirmedResult = await _unitOfWork.ConfirmedResults.GetByID(command.Id);
                ConfirmedResult.IsDeleted = true;
                _unitOfWork.ConfirmedResults.Update(ConfirmedResult);
                try
                {
                    await _unitOfWork.CompleteAsync();

                    return $"با موفقیت حذف شد";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }



            }
        }
    }
}
