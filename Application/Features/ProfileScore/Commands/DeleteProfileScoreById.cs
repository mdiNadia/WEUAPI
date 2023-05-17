using Application.Errors;
using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ProfileScore.Commands
{
    public class DeleteProfileScoreById : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteProfileScoreByIdHandler : IRequestHandler<DeleteProfileScoreById, int>
        {
            private readonly IMediator _mediator;
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteProfileScoreByIdHandler(IMediator mediator, IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(DeleteProfileScoreById command, CancellationToken cancellationToken)
            {

                var profileScore = await _unitOfWork.ProfileScores.GetQueryList()
               .Where(c => c.Id == command.Id).Include(c => c.Icon).FirstOrDefaultAsync();
                if (profileScore == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                if (profileScore.IconId != null)
                    await _mediator.Send(new DeleteAttachmentById { Id = (int)profileScore.IconId, FolderName = "/ProfileScore" });
                _unitOfWork.ProfileScores.Delete(profileScore);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return command.Id;
                }
                catch (Exception)
                {

                    throw new Exception("خطا در ذخیره اطلاعات!");
                }




            }
        }
    }
}
