using Application.Errors;
using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Application.Features.Role.Commands
{
    public class UpdateRole : IRequest<int>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public class UpdateRoleHandler : IRequestHandler<UpdateRole, int>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IMediator _mediator;
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public UpdateRoleHandler(RoleManager<IdentityRole> roleManager, IMediator mediator, IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._roleManager = roleManager;
                this._mediator = mediator;
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateRole command, CancellationToken cancellationToken)
            {
                IdentityRole role = await _roleManager.FindByIdAsync(command.Id);
                if (role == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                else
                {
                    role.Name = command.Name;
                    try
                    {
                        var result = await _roleManager.UpdateAsync(role);
                        if (result.Succeeded) { return 1; }
                        return 0;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
                }

            }
        }
    }
}
