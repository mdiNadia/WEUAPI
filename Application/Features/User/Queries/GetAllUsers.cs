using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.User.Queries
{
    public class GetAllUsers : IRequest<IEnumerable<GetUserDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllUsers(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllUsersHandler : IRequestHandler<GetAllUsers, IEnumerable<GetUserDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllUsersHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetUserDto>> Handle(GetAllUsers query, CancellationToken cancellationToken)
            {

                var UserList = await _unitOfWork.Users.GetQueryList().AsNoTracking()
                    .Include(c => c.Profiles)
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .Select(c => new GetUserDto()
                    {
                        Id = c.Id,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        UserName = c.UserName,
                        CreationDate = c.CreationDate,
                        ProfileId = c.Profiles.Select(c => c.Id).FirstOrDefault(),
                        ProfileUsername = c.Profiles.Select(c => c.Username).FirstOrDefault(),
                    }).ToListAsync();
                if (UserList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return UserList.AsReadOnly();


            }
        }
    }
}
