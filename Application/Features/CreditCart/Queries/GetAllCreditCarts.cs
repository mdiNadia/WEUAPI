using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.CreditCart.Queries
{
    public class GetAllCreditCarts : IRequest<IEnumerable<GetCreditCartDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllCreditCarts(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllCreditCartsHandler : IRequestHandler<GetAllCreditCarts, IEnumerable<GetCreditCartDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCreditCartsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetCreditCartDto>> Handle(GetAllCreditCarts query, CancellationToken cancellationToken)
            {

                var creditCartList = await _unitOfWork.BankAccounts.GetQueryList()
                    .Include(c => c.Wallet)
                    .AsNoTracking()
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .Select(c => new GetCreditCartDto
                    {
                        CardName = c.CardName,
                        CardNumber = c.CardNumber,
                        CardShebaNumber = c.CardShebaNumber,
                        Expiredate = c.Expiredate,
                        IsDeleted = c.IsDeleted,
                        Wallet = new GetNameAndId
                        {
                            Id = c.WalletId,
                            Name = c.Wallet.Name,
                        },
                        CreationDate = c.CreationDate,
                    })
                   .ToListAsync();
                if (creditCartList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return creditCartList;


            }
        }
    }
}
