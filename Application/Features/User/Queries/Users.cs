using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.User.Queries
{
    public class Users : IRequest<List<GetNameAndIdString>>
    {

        public class UsersHandler : IRequestHandler<Users, List<GetNameAndIdString>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UsersHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndIdString>> Handle(Users query, CancellationToken cancellationToken)
            {

                var users = await _unitOfWork.Users.GetQueryList()
                    .AsNoTracking()
                    .Select(c => new GetNameAndIdString()
                    {
                        Id = c.Id,
                        Name = c.UserName,
                        CreationDate = c.CreationDate,

                    }).OrderByDescending(c => c.CreationDate).ToListAsync();
                if (users == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                return users;


            }
        }
    }
}
