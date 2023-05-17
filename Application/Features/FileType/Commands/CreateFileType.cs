using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;

namespace Application.Features.FileType.Commands
{
    public class CreateFileType : IRequest<int>
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public class CreateFileTypeHandler : IRequestHandler<CreateFileType, int>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateFileTypeHandler(IMediator mediator, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateFileType command, CancellationToken cancellationToken)
            {

                var fileType = new Domain.Entities.FileType();
                fileType.Name = command.Name;
                fileType.Type = (Domain.Enums.FileType)command.Type;
                fileType.Extension = command.Extension;
                fileType.Size = command.Size;
                fileType.CreationDate = DateTime.Now;
                _unitOfWork.FileTypes.Insert(fileType);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return fileType.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }





            }
        }
    }
}
