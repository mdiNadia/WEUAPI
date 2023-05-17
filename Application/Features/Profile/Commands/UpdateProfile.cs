using Application.Errors;
using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Profile.Commands
{
    public class UpdateProfile : IRequest<int>
    {
        //public int Id { get; set; }
        public IFormFile? AvatarFile { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public string? Link { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender? Gender { get;set; }
        public ProfileType? ProfileType { get; set; }
        public class UpdateProfileHandler : IRequestHandler<UpdateProfile, int>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public UpdateProfileHandler(IUserAccessor userAccessor,IMediator mediator, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._userAccessor = userAccessor;
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;

            }
            public async Task<int> Handle(UpdateProfile command, CancellationToken cancellationToken)
            {
                var profile = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());

                if (profile == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                ///https://barnamenevisan.org/Articles/Article4824.html
                ///https://virgool.io/@javadjahangiri/%D8%A7%D9%84%DA%AF%D9%88%DB%8C-unitofwork-pattern-%D8%AF%D8%B1-c-aspnet-core-hobqvq07tl6v
                ///https://stackoverflow.com/questions/6028626/ef-code-first-dbcontext-and-transactions
                ///https://stackoverflow.com/questions/34609038/transactions-in-unit-of-work-design-pattern
                ///https://learn.microsoft.com/en-gb/ef/ef6/saving/transactions?redirectedfrom=MSDN

                if (command.AvatarFile != null)
                {
                    if (profile.AvatarId != null)
                    {

                        //آپدیت عکس پروفایل
                        UpdateAttachment updateAttachment = new UpdateAttachment();
                        updateAttachment.File = command.AvatarFile;
                        updateAttachment.Id = (int)profile.AvatarId;
                        updateAttachment.FolderName = "/Images/Profile";
                        await _mediator.Send(updateAttachment);
                    }
                    else
                    {
                        CreateAttachment createAttachment = new CreateAttachment();
                        createAttachment.Name = command.Name ?? "";
                        createAttachment.Description = "";
                        //createAttachment.FileType = 0;
                        createAttachment.File = command.AvatarFile;
                        createAttachment.FolderName = "/Images/Profile";
                        var avatarID = await _mediator.Send(createAttachment);
                        profile.AvatarId = avatarID;
                    }
                }
                profile.Name = command.Name ?? profile.Name;
                profile.Bio = command.Bio ?? profile.Bio;
                profile.Link = command.Link ?? profile.Link;
                profile.Gender = command.Gender ?? Domain.Enums.Gender.male;
                profile.Birthday = command.Birthday?? profile.Birthday;
                profile.ProfileTypeEnum = command.ProfileType ?? profile.ProfileTypeEnum;
                _unitOfWork.Profiles.Update(profile);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return profile.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }






            }
        }
    }
}
