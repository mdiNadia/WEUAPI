using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Role.Queries
{
    public class GetAllRoles : IRequest<List<IdentityRole>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllRoles(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllRolesHandler : IRequestHandler<GetAllRoles, List<IdentityRole>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IUnitOfWork _unitOfWork;

            public GetAllRolesHandler(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
            {
                this._roleManager = roleManager;
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<IdentityRole>> Handle(GetAllRoles query, CancellationToken cancellationToken)
            {
                List<IdentityRole> roles = await _roleManager.Roles
                                   .OrderByDescending(r => r.Id)
                                   .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                                   .Take(query._filter.PageSize)
                                   .ToListAsync();
                if (roles == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return roles;
            }
        }
    }
}
