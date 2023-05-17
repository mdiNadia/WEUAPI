using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Currency.Queries
{
    public class GetAllCountCreditCarts : IRequest<int>
    {
        public class GetAllCountCreditCartsHandler : IRequestHandler<GetAllCountCreditCarts, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountCreditCartsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountCreditCarts query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.BankAccounts.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {
                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
