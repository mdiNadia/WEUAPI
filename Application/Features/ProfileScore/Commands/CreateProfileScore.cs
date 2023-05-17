using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ProfileScore.Commands
{
    public class CreateProfileScore : IRequest<int>
    {
        public int ProfileType { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public IFormFile IconFile { get; set; }


        public class CreateProfileScoreHandler : IRequestHandler<CreateProfileScore, int>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateProfileScoreHandler(IMediator mediator, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateProfileScore command, CancellationToken cancellationToken)
            {

                var profileScore = new Domain.Entities.ProfileScore();
                if (command.IconFile != null)
                {
                    //در جدول فایل ها اول ایجاد میشه آیدی ای که از فایل برمیگردد اینجا در آیکون قرار میگیرد
                    CreateAttachment createAttachment = new CreateAttachment();
                    createAttachment.Name = command.Name;
                    createAttachment.Description = "آیکونِ" + command.Name;
                    //createAttachment.FileType = 0;
                    createAttachment.File = command.IconFile;
                    createAttachment.FolderName = "/Images/ProfileScore";
                    var iconID = await _mediator.Send(createAttachment);
                    ////////////////////////////////////////////////////
                    profileScore.IconId = iconID;
                }
                profileScore.CreationDate = DateTime.Now;
                profileScore.ProfileType = (ProfileType)command.ProfileType;
                profileScore.Name = command.Name;
                profileScore.Score = Convert.ToInt16(command.Score);
                _unitOfWork.ProfileScores.Insert(profileScore);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return profileScore.Id;
                }
                catch (Exception)
                {

                    throw new Exception("خطا در ذخیره اطلاعات!");
                }




            }
        }
    }
}
