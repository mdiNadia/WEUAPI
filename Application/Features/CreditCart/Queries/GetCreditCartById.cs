using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.CreditCart.Queries
{
    public class GetCreditCartById : IRequest<GetCreditCartDto>
    {
        public int Id { get; set; }
        public class GetCreditCartByIdHandler : IRequestHandler<GetCreditCartById, GetCreditCartDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCreditCartByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetCreditCartDto> Handle(GetCreditCartById query, CancellationToken cancellationToken)
            {

                var creditCart = await _unitOfWork.BankAccounts.GetQueryList()
                    .Include(c => c.Wallet)
                    .AsNoTracking()
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
                    }).FirstOrDefaultAsync();
                if (creditCart == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return creditCart;


            }
        }
    }
}
