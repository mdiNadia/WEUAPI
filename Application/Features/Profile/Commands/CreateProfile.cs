using Application.Errors;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Profile.Commands
{
    public class CreateProfile : IRequest<int>
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public class CreateProfileHandler : IRequestHandler<CreateProfile, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateProfileHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(CreateProfile command, CancellationToken cancellationToken)
            {

                var IsExcistUsername = await _unitOfWork.Profiles.GetQueryList()
                    .AsNoTracking().SingleOrDefaultAsync(c => c.Username == command.Username);
                if (IsExcistUsername != null)
                    throw new RestException(HttpStatusCode.BadRequest, "this username have already Profile!");
                else
                {
                    var profile = new Domain.Entities.Profile();
                    profile.UserId = command.UserId;
                    profile.Username = command.Username;
                    profile.ProfileTypeEnum = ProfileType.personal;
                    profile.CreationDate = DateTime.Now;
                    _unitOfWork.Profiles.Insert(profile);
                    //Add ProfileSetting
                    var defaultLang = await _unitOfWork.Languages.GetQueryList()
                        .AsNoTracking().FirstOrDefaultAsync(c => c.IsDefault);
                    var profileSetting = new ProfileSetting();
                    profileSetting.DateCreated = DateTime.Now;
                    profileSetting.UserName = command.Username;
                    profileSetting.ProfileId = profile.Id;
                    profileSetting.UserName = command.Username;
                    profileSetting.Language = defaultLang.ShortName;
                    _unitOfWork.ProfileSettings.Insert(profileSetting);
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
}
