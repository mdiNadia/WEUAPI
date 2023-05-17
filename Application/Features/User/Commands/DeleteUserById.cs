using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.User.Commands
{
    public class DeleteUserById : IRequest<string>
    {
        public string Id { get; set; }
        public class DeleteUserByIdHandler : IRequestHandler<DeleteUserById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteUserByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteUserById command, CancellationToken cancellationToken)
            {
                var User = await _unitOfWork.Users.GetByID(command.Id);
                if (User == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                _unitOfWork.Wallets.Delete(command.Id);
                _unitOfWork.Profiles.Delete(User.UserName);
                _unitOfWork.Users.Delete(User);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{User.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
