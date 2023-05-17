using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.AdCategory.Queries
{
    public class Advertisings : IRequest<List<GetNameAndId>>
    {
        public class AdvertisingsHandler : IRequestHandler<Advertisings, List<GetNameAndId>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public AdvertisingsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndId>> Handle(Advertisings query, CancellationToken cancellationToken)
            {
                var ads = await _unitOfWork
                    .Advertisings.GetQueryList()
                    .AsNoTracking()
                    .Select(c => new GetNameAndId()
                    { Id = c.Id, Name = c.Name, CreationDate = c.CreationDate })
                    .ToListAsync();
                if (ads == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "آگهی وجود ندارد!");

                }

                return ads;


            }
        }
    }
}
