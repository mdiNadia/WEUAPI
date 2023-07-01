using Application.Dtos.Account;
using Application.Dtos.Register;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;

namespace Application.Features.User.Commands
{
    public class CreateUser : IRequest<RegisterResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } = "User";
        public class CreateUserHandler : IRequestHandler<CreateUser, RegisterResult>
        {

            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public CreateUserHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }
            public async Task<RegisterResult> Handle(CreateUser command, CancellationToken cancellationToken)
            {

                RegisterModel register = new RegisterModel();
                register.PhoneNumber = command.PhoneNumber;
                register.NumCode = "+98";
                register.Username = command.Username;
                register.Email = command.Email;
                register.FirstName = command.FirstName;
                register.LastName = command.LastName;
                register.Password = command.Password;

                register.Role = command.Role;
                try
                {
                    var result = await _userAccessor.RegisterAsync(register);
                    return result;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
