using Application.Interfaces;
using MediatR;

namespace Application.Features.Transaction.Commands
{
    public class CreateTransactionStatus : IRequest<int>
    {
        public int TransactionId { get; set; }
        public Domain.Enums.TransactionStatus TransactionStatusEnum { get; set; }

        public class CreateTransactionStatusHandler : IRequestHandler<CreateTransactionStatus, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateTransactionStatusHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(CreateTransactionStatus command, CancellationToken cancellationToken)
            {


                var transactionStatus = new Domain.Entities.TransactionStatus();
                transactionStatus.TransactionId = command.TransactionId;
                transactionStatus.TransactionStatusEnum = command.TransactionStatusEnum;
                transactionStatus.CreationDate = DateTime.Now;
                _unitOfWork.TransactionStatuses.Insert(transactionStatus);

                try
                {
                    await _unitOfWork.CompleteAsync();
                    return transactionStatus.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
