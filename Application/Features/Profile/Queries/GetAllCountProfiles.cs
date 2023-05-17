using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Profile.Queries
{
    public class GetAllCountProfiles : IRequest<int>
    {
        public class GetAllCountProfilesHandler : IRequestHandler<GetAllCountProfiles, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountProfilesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountProfiles query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Profiles.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
