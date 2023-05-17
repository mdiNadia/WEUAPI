using Application.Errors;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.User.Commands
{
    public class DeleteUserByNumber : IRequest<string>
    {
        public string number { get; set; }
        public class DeleteUserByNumberHandler : IRequestHandler<DeleteUserByNumber, string>
        {
            private readonly IMediator _mediator;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteUserByNumberHandler(IMediator mediator,UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._userManager = userManager;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteUserByNumber command, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.Users.GetQueryList()
                    .FirstOrDefaultAsync(c=>c.PhoneNumber == command.number);
                var userRoles = _userManager.GetRolesAsync(user).Result.ToList();
                if (user == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                _unitOfWork.Wallets.Delete(user.Id);
                //_unitOfWork.Profiles.Delete(user.UserName);
                await _mediator.Send(new DeleteProfileByUserName { username = user.UserName});
                foreach (var role in userRoles) { await _userManager.RemoveFromRoleAsync(user, role); }
                _unitOfWork.Users.Delete(user);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{user.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
