using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Role.Commands
{
    public class CreateRole : IRequest<int>
    {
        public string Name { get; set; }
        public class CreateRoleHandler : IRequestHandler<CreateRole, int>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;

            public CreateRoleHandler(RoleManager<IdentityRole> roleManager, IMediator mediator, IUnitOfWork unitOfWork)
            {
                this._roleManager = roleManager;
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateRole command, CancellationToken cancellationToken)
            {

                try
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(command.Name));
                    if (result.Succeeded) { return 1; }
                    return 0;

                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
