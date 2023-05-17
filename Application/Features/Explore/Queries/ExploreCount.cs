using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Explore.Queries
{
    public class ExploreCount : IRequest<int>
    {
        /// <summary>
        /// CategoryId as string
        /// </summary>
        public string? id { get; set; }
        public class ExploreCountHandler : IRequestHandler<ExploreCount, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ExploreCountHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(ExploreCount query, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.ConfirmedResults.GetQueryList()
                                 .AsNoTracking()
                                 .Where(c => c.Categories == query.id && c.IsActive)
                                 .CountAsync();
                    return result;
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");
                }


            }
        }
    }
}
