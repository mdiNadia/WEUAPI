using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.Queries
{
    public class GetAllUsers : IRequest<IQueryable<GetUserDto>>
    {
        public GetAllUsers()
        {

        }
        public class GetAllUsersHandler : IRequestHandler<GetAllUsers, IQueryable<GetUserDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllUsersHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetUserDto>> Handle(GetAllUsers query, CancellationToken cancellationToken)
            {
                var userList = _unitOfWork.Users.GetQueryList().AsNoTracking()
                    .Include(c => c.Profiles)
                    .OrderByDescending(c => c.CreationDate)
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
                    });

                return userList;
            }
        }
    }
}
