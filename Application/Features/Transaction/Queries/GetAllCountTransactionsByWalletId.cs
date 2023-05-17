using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Transaction.Queries
{
    public class GetAllCountTransactionsByWalletId : IRequest<int>
    {
        public int Id { get; set; }
        public class GetAllCountTransactionsByWalletIdHandler : IRequestHandler<GetAllCountTransactionsByWalletId, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountTransactionsByWalletIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountTransactionsByWalletId query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Transactions.GetQueryList().AsNoTracking()
                       .Where(c => c.WalletId == query.Id).CountAsync();
                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }


            }
        }
    }
}
