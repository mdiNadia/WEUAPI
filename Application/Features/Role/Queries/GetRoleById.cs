using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Role.Queries
{
    public class GetRoleById : IRequest<IdentityRole>
    {
        public string Id { get; set; }
        public class GetRoleByIdHandler : IRequestHandler<GetRoleById, IdentityRole>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IUnitOfWork _unitOfWork;

            public GetRoleByIdHandler(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
            {
                this._roleManager = roleManager;
                this._unitOfWork = unitOfWork;
            }
            public async Task<IdentityRole> Handle(GetRoleById query, CancellationToken cancellationToken)
            {

                var role = await _roleManager.Roles
                    .Where(c => c.Id == query.Id)
                    .FirstOrDefaultAsync();
                if (role == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return role;


            }
        }
    }
}
