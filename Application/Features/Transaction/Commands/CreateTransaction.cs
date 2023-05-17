using Application.Interfaces;
using Application.Services.FileStorage;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Transaction.Commands
{
    public class CreateTransaction : IRequest<int>
    {
        public int WalletId { get; set; }
        public int Coin { get; set; }
        public string Description { get; set; }
        public char Sign { get; set; }
        public Domain.Enums.WalletType TransactionType { get; set; }
        public class CreateTransactionHandler : IRequestHandler<CreateTransaction, int>
        {

            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateTransactionHandler(IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {

                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateTransaction command, CancellationToken cancellationToken)
            {

                Domain.Entities.Transaction transaction = new Domain.Entities.Transaction();
                transaction.CreationDate = DateTime.Now;
                transaction.Description = command.Description;
                transaction.WalletId = command.WalletId;
                _unitOfWork.Transactions.Insert(transaction);


                Domain.Entities.TransactionType transactionType = new Domain.Entities.TransactionType();
                transactionType.TransactionTypeEnum = command.TransactionType;
                transactionType.TransactionId = transaction.Id;
                 _unitOfWork.TransactionTypes.Insert(transactionType);


                Domain.Entities.TransactionStatus transactionStatus = new Domain.Entities.TransactionStatus();
                transactionStatus.TransactionId = transaction.Id;
                _unitOfWork.TransactionStatuses.Insert(transactionStatus);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return transaction.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }




            }
        }
    }
}
