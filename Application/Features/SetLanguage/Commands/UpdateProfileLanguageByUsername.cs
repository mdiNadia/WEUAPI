using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.SetLanguage.Commands
{
    public class UpdateProfileLanguageByUsername : IRequest<string>
    {

        public string Language { get; set; }

        public class UpdateProfileLanguageByUsernameHandler : IRequestHandler<UpdateProfileLanguageByUsername, string>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public UpdateProfileLanguageByUsernameHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(UpdateProfileLanguageByUsername command, CancellationToken cancellationToken)
            {
                var profileSettig = await _unitOfWork.ProfileSettings
                      .GetQueryList().FirstOrDefaultAsync(c => c.UserName == _userAccessor.GetCurrentUserNameAsync());
                if (profileSettig == null) throw new RestException(HttpStatusCode.NotFound, "اطلاعات کاربر وجود ندارد!");
                profileSettig.Language = command.Language;
                _unitOfWork.ProfileSettings.Update(profileSettig);
                await _unitOfWork.CompleteAsync();
                return profileSettig.Language;

            }
        }
    }
}
