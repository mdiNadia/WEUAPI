using Application.Errors;
using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Language.Commands
{
    public class UpdateLanguage : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int direction { get; set; }
        public IFormFile? IconFile { get; set; }
        public string? Icon { get; set; }
        public bool IsDefault { get; set; }

        public class UpdateLanguageHandler : IRequestHandler<UpdateLanguage, int>
        {
            private readonly IMediator _mediator;
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public UpdateLanguageHandler(IMediator mediator, IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateLanguage command, CancellationToken cancellationToken)
            {
                var shortNameIsExcist = await _unitOfWork.Languages.GetQueryList().AsNoTracking()
                .AnyAsync(c => c.ShortName == command.ShortName && c.Id != command.Id);
                if (shortNameIsExcist)
                    throw new RestException(HttpStatusCode.BadRequest, "ShortName is already excist!");
                var language = await _unitOfWork.Languages.GetByID(command.Id);

                if (language == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                else
                {
                    if (command.IconFile != null)
                    {
                        //آپدیت آیکون
                        if (language.IconId != null)
                        {

                            UpdateAttachment updateAttachment = new UpdateAttachment();
                            updateAttachment.File = command.IconFile;
                            updateAttachment.Id = (int)language.IconId;
                            updateAttachment.FolderName = "/Images/Language";
                            await _mediator.Send(updateAttachment);
                        }
                        else
                        {
                            CreateAttachment createAttachment = new CreateAttachment();
                            createAttachment.File = command.IconFile;
                            createAttachment.FolderName = "/Images/Language";
                            //createAttachment.FileType = 0;
                            createAttachment.Name = command.Name;
                            createAttachment.Description = command.Name;
                            var iconID = await _mediator.Send(createAttachment);
                            language.IconId = iconID;

                        }
                    }
                    language.Name = command.Name ?? language.Name;
                    language.ShortName = command.ShortName ?? language.ShortName;
                    language.Direction = (Direction)command.direction;
                    if (command.IsDefault)
                    {
                        var isDefault = await _unitOfWork.Languages.GetQueryList()
                             .Where(c => c.IsDefault).ToListAsync();
                        if (isDefault.Any())
                        {
                            isDefault.ForEach(c =>
                            {
                                c.IsDefault = false;
                                _unitOfWork.Languages.Update(c);
                            });
                            await _unitOfWork.CompleteAsync();
                        }
                    }
                    language.IsDefault = command.IsDefault;
                    _unitOfWork.Languages.Update(language);

                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return language.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }


            }
        }
    }
}
