using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Transaction.Queries
{
    public class GetAllCountTransactions : IRequest<int>
    {
        public class GetAllCountTransactionsHandler : IRequestHandler<GetAllCountTransactions, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountTransactionsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountTransactions query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Transactions.GetQueryList().AsNoTracking().AsNoTracking().CountAsync();
                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }


            }
        }
    }
}
