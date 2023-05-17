using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace Application.Features.Role.Queries
{
    public class GetAll : IRequest<List<GetNameAndIdString>>
    {
        public class GetAllHandler : IRequestHandler<GetAll, List<GetNameAndIdString>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IUnitOfWork _unitOfWork;

            public GetAllHandler(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
            {
                this._roleManager = roleManager;
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndIdString>> Handle(GetAll query, CancellationToken cancellationToken)
            {
                List<GetNameAndIdString> roles = await _roleManager.Roles
                                         .Select(c => new GetNameAndIdString
                                         {
                                             Id = c.Id,
                                             Name = c.Name
                                         })
                                         .OrderByDescending(r => r.Id).ToListAsync();
                if (roles == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return roles;
            }
        }
    }
}
