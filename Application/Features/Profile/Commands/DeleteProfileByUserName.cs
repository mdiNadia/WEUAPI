using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.User.Commands
{
    public class DeleteProfileByUserName : IRequest<string>
    {
        public string username { get; set; }
        public class DeleteProfileByUserNameHandler : IRequestHandler<DeleteProfileByUserName, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteProfileByUserNameHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteProfileByUserName command, CancellationToken cancellationToken)
            {
                var profile = await _unitOfWork.Profiles.GetQueryList().
                    FirstOrDefaultAsync(c => c.Username == command.username);
                if (profile == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                _unitOfWork.Profiles.Delete(profile);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{profile.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
