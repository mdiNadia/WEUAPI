using Application.Errors;
using Application.Features.Attachment.Commands;
using Application.Interfaces;

using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Language.Commands
{
    public class CreateLanguage : IRequest<int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Direction { get; set; }
        public IFormFile IconFile { get; set; }
        public bool IsDefault { get; set; }
        public class CreateLanguageHandler : IRequestHandler<CreateLanguage, int>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;

            public CreateLanguageHandler(IMediator mediator, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateLanguage command, CancellationToken cancellationToken)
            {

                var shortNameIsExcist = await _unitOfWork.Languages.GetQueryList().AsNoTracking()
                    .AnyAsync(c => c.ShortName == command.ShortName);
                if (shortNameIsExcist)
                    throw new RestException(HttpStatusCode.BadRequest, "ShortName is already excist!");
                var language = new Domain.Entities.Language();
                if (command.IconFile != null)
                {
                    //در جدول فایل ها اول ایجاد میشه آیدی ای که از فایل برمیگردد اینجا در آیکون قرار میگیرد
                    CreateAttachment createAttachment = new CreateAttachment();
                    createAttachment.Name = command.Name;
                    createAttachment.Description = "آیکونِ" + command.Name;
                    //createAttachment.FileTypeId = 0;
                    createAttachment.File = command.IconFile;
                    createAttachment.FolderName = "/Images/Language";
                    var iconID = await _mediator.Send(createAttachment);
                    ////////////////////////////////////////////////////
                    language.IconId = iconID;
                }
                language.Name = command.Name;
                language.ShortName = command.ShortName;
                language.Direction = (Direction)command.Direction;
                language.CreationDate = DateTime.Now;
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
                _unitOfWork.Languages.Insert(language);
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
