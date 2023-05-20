using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Application.Features.Transaction.Commands
{
    public class CreateTransaction : IRequest<int>
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int Value { get; set; }//coin
        public Domain.Enums.TransactionType TransactionType { get; set; } // واریز یا برداشت


        public class CreateTransactionHandler : IRequestHandler<CreateTransaction, int>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public CreateTransactionHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(CreateTransaction command, CancellationToken cancellationToken)
            {
                #region User Info
                var profile = await _unitOfWork.Profiles.GetQueryList()
                   .FirstOrDefaultAsync(c => c.Username == _userAccessor.GetCurrentUserNameAsync());
                var wallet = await _unitOfWork.Wallets.GetQueryList().FirstAsync(c => c.Profile == profile);
                #endregion
                #region Transaction
                Domain.Entities.Transaction transaction = new Domain.Entities.Transaction();
                transaction.CreationDate = DateTime.Now;
                transaction.Description = command.Description;
                transaction.Wallet = wallet;
                transaction.Amount = command.Amount;
                if (command.TransactionType == Domain.Enums.TransactionType.deposit)
                    transaction.TransactionSign = '+';
                else
                    transaction.TransactionSign = '-';
                _unitOfWork.Transactions.Insert(transaction);
                #endregion
                #region TransactionType
                Domain.Entities.TransactionType transactionType = new Domain.Entities.TransactionType();
                transactionType.TransactionTypeEnum = command.TransactionType;
                transactionType.Transaction = transaction;
                _unitOfWork.TransactionTypes.Insert(transactionType);
                #endregion
                #region Payment
                var response = true;//paymentCreate ====> Amount
                #endregion
                #region TransactionStatus And Wallet Changes
                Domain.Entities.TransactionStatus transactionStatus = new Domain.Entities.TransactionStatus();
                if (response)
                {
                    transactionStatus.TransactionStatusEnum = Domain.Enums.TransactionStatus.Success;
                    #region Wallet Changes
                    if (command.TransactionType == Domain.Enums.TransactionType.deposit)
                        wallet.Value += command.Value;
                    else
                        wallet.Value = 0;

                    _unitOfWork.Wallets.Update(wallet);
                    #endregion
                }
                else
                {
                    transactionStatus.TransactionStatusEnum = Domain.Enums.TransactionStatus.Faild;
                }
                transactionStatus.Transaction = transaction;
                _unitOfWork.TransactionStatuses.Insert(transactionStatus);
                #endregion
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
