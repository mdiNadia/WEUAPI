using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Wallet.Queries
{
    public class GetAllWallets : IRequest<IEnumerable<GetWalletDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllWallets(IPaginationFilter filter)
        {
            _filter = filter;
        }

        public class GetAllWalletsHandler : IRequestHandler<GetAllWallets, IEnumerable<GetWalletDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllWalletsHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetWalletDto>> Handle(GetAllWallets query, CancellationToken cancellationToken)
            {

                var walletList = await _unitOfWork.Wallets.GetQueryList()
                    .Include(c => c.Profile)
                    .Include(c => c.Currency)
                    .AsNoTracking()
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .Select(c => new GetWalletDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        IsActive = c.IsActive,
                        UpdateDate = c.UpdateDate,
                        CreationDate = c.CreationDate,
                        TotalCredit = c.TotalCredit,
                        Currency = new GetNameAndId()
                        {
                            Id = c.Currency.Id,
                            Name = c.Currency.CurrencyName,
                        },
                        User = new GetNameAndId()
                        {
                            Id = c.Profile.Id,
                            Name = c.Profile.Username,
                        },

                    }).ToListAsync();
                if (walletList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                return walletList.AsReadOnly();


            }
        }
    }
}
