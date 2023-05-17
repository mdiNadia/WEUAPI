using Application.Errors;
using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Application.Features.Role.Commands
{
    public class DeleteRoleById : IRequest<string>
    {
        public string Id { get; set; }

        public class DeleteRoleByIdHandler : IRequestHandler<DeleteRoleById, string>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteRoleByIdHandler(RoleManager<IdentityRole> roleManager, IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._roleManager = roleManager;
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteRoleById command, CancellationToken cancellationToken)
            {

                IdentityRole role = await _roleManager.FindByIdAsync(command.Id);
                if (role == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                try
                {
                    IdentityResult result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                        return $"{role.Id}";
                    else
                        return $"{role.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
