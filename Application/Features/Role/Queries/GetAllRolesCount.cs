using Application.Errors;
using Application.Interfaces;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Role.Queries
{
    public class GetAllCountRoles : IRequest<int>
    {
        public class GetAllCountRolesHandler : IRequestHandler<GetAllCountRoles, int>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountRolesHandler(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
            {
                this._roleManager = roleManager;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountRoles query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _roleManager.Roles.CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
