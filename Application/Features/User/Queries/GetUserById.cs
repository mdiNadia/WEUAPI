using Application.Dtos.User;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.User.Queries
{
    public class GetUserById : IRequest<GetUserDto>
    {
        public string Id { get; set; }
        public class GetUserByIdHandler : IRequestHandler<GetUserById, GetUserDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetUserByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetUserDto> Handle(GetUserById query, CancellationToken cancellationToken)
            {

                var User = await _unitOfWork.Users
                   .GetQueryList().AsNoTracking().Where(c => c.Id == query.Id)
                   .Include(c => c.Profiles)
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
                   }).SingleOrDefaultAsync();
                if (User == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return User;


            }
        }
    }
}
