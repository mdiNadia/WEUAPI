using Application.Errors;
using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;
using System.Net;

namespace Application.Features.Language.Commands
{
    public class DeleteLanguageById : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteLanguageByIdHandler : IRequestHandler<DeleteLanguageById, string>
        {
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteLanguageByIdHandler(IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteLanguageById command, CancellationToken cancellationToken)
            {

                var language = await _unitOfWork.Languages.GetByID(command.Id);
                if (language == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                if (language.IsDefault) throw new RestException(HttpStatusCode.BadRequest, "زبان پیش فرض را تغییر دهید!");
                _unitOfWork.Languages.Delete(language);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{language.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
