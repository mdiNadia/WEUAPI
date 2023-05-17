using Application.Interfaces;
using MediatR;

namespace Application.Features.Transaction.Commands
{
    public class CreateTransactionType : IRequest<int>
    {
        public int TransactionId { get; set; }
        public Domain.Enums.WalletType TransactionTypeEnum { get; set; }

        public class CreateTransactionTypeHandler : IRequestHandler<CreateTransactionType, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateTransactionTypeHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(CreateTransactionType command, CancellationToken cancellationToken)
            {


                var transactionType = new Domain.Entities.TransactionType();
                transactionType.TransactionId = command.TransactionId;
                transactionType.TransactionTypeEnum = command.TransactionTypeEnum;
                transactionType.CreationDate = DateTime.Now;
                _unitOfWork.TransactionTypes.Insert(transactionType);

                try
                {
                    await _unitOfWork.CompleteAsync();
                    return transactionType.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
