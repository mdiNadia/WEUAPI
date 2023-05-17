using Application.Errors;
using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ProfileScore.Commands
{
    public class UpdateProfileScore : IRequest<int>
    {
        public int Id { get; set; }
        public int ProfileType { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public IFormFile? IconFile { get; set; }
        public string? Icon { get; set; }
        public class UpdateProfileScoreHandler : IRequestHandler<UpdateProfileScore, int>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public UpdateProfileScoreHandler(IMediator mediator, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }
            public async Task<int> Handle(UpdateProfileScore command, CancellationToken cancellationToken)
            {

                var profileScore = await _unitOfWork.ProfileScores.GetQueryList()
                    .Where(a => a.Id == command.Id)
                    .FirstOrDefaultAsync();

                if (profileScore == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                else
                {
                    if (command.IconFile != null)
                    {
                        //آپدیت آیکون
                        if (profileScore.IconId != null)
                        {

                            UpdateAttachment updateAttachment = new UpdateAttachment();
                            updateAttachment.File = command.IconFile;
                            updateAttachment.Id = (int)profileScore.IconId;
                            updateAttachment.FolderName = "/Images/ProfileScore";
                            await _mediator.Send(updateAttachment);
                        }
                        else
                        {
                            CreateAttachment createAttachment = new CreateAttachment();
                            createAttachment.File = command.IconFile;
                            createAttachment.FolderName = "/Images/ProfileScore";
                            //createAttachment.FileType = 0;
                            createAttachment.Name = command.Name;
                            createAttachment.Description = command.Name;
                            var iconID = await _mediator.Send(createAttachment);
                            profileScore.IconId = iconID;

                        }
                    }
                    profileScore.ProfileType = (ProfileType)command.ProfileType;
                    profileScore.Name = command.Name;
                    profileScore.Score = command.Score;
                    _unitOfWork.ProfileScores.Update(profileScore);

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
}
