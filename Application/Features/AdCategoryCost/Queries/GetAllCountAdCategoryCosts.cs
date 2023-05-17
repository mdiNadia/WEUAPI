using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.AdCategoryCost.Queries
{
    public class GetAllCountAdCategoryCosts : IRequest<int>
    {
        public class GetAllCountAdCategoryCostsHandler : IRequestHandler<GetAllCountAdCategoryCosts, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountAdCategoryCostsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountAdCategoryCosts query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.AdCategoryCosts
                        .GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception err)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، این خطا مربوط به سرویس ارائه دهنده میباشد!");
                }
            }
        }
    }
}
