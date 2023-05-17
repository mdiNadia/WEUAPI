using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.FileType.Commands
{
    public class UpdateFileType : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public class UpdateFileTypeHandler : IRequestHandler<UpdateFileType, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateFileTypeHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateFileType command, CancellationToken cancellationToken)
            {

                var fileType = await _unitOfWork.FileTypes.GetByID(command.Id);

                if (fileType == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                else
                {
                    fileType.Name = command.Name;
                    fileType.Extension = command.Extension;
                    fileType.Size = command.Size;
                    fileType.Type = (Domain.Enums.FileType)command.Type;


                    _unitOfWork.FileTypes.Update(fileType);
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
}
