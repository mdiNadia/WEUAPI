using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Transaction.Queries
{
    public class GetAllTransactionsByWalletId : IRequest<IEnumerable<GetTransactionDto>>
    {
        private readonly IPaginationFilter _filter;
        private readonly int _id;
        public GetAllTransactionsByWalletId(IPaginationFilter filter, int id)
        {
            _filter = filter;
            _id = id;
        }
        public class GetAllTransactionsByWalletIdHandler : IRequestHandler<GetAllTransactionsByWalletId, IEnumerable<GetTransactionDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllTransactionsByWalletIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetTransactionDto>> Handle(GetAllTransactionsByWalletId query, CancellationToken cancellationToken)
            {

                var transactionList = await _unitOfWork.Transactions.GetQueryList()
                    .Include(c => c.Wallet).Include(c => c.TransactionType)
                    .AsNoTracking()
                    .Where(c => c.WalletId == query._id)
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .Select(c => new GetTransactionDto()
                    {
                        Id = c.Id,
                        Description = c.Description,
                        CreationDate = c.CreationDate,
                        Wallet = new Dtos.Common.GetNameAndId()
                        {
                            Id = c.WalletId,
                            Name = c.Wallet.Name
                        },
                        TransactionType = new Dtos.Common.GetNameAndId()
                        {
                            Id = c.TransactionTypeId,
                            Name = c.TransactionType.TransactionTypeEnum.ToString()
                        }

                    }).ToListAsync();
                if (transactionList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return transactionList.AsReadOnly();


            }
        }
    }
}
