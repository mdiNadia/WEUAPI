using Application.Errors;
using Application.Features.Order.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.Net;

namespace Application.Features.Transaction.Commands
{
    public class StartTransaction : IRequest<int>
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Domain.Enums.WalletType TransactionType { get; set; }
        public Domain.Enums.OrderType OrderType { get; set; } //wallet OR shopping
        public class StartTransactionHandler : IRequestHandler<StartTransaction, int>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public StartTransactionHandler(IUserAccessor userAccessor, IMediator mediator, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._userAccessor = userAccessor;
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(StartTransaction command, CancellationToken cancellationToken)
            {
                var target = await _unitOfWork.Profiles.GetQueryList().FirstAsync(c=>c.Username == _userAccessor.GetCurrentUserNameAsync());
                
                using (var dbContextTransaction = _unitOfWork.BeginTransaction())
                {

                    try
                    {
                        #region افزودن یک سفارش
                        CreateOrderRow order = new CreateOrderRow();
                        order.TransactionType = command.TransactionType;
                        order.Name = target.Username;
                        order.TargetId = target.Id;
                        order.Description = command.Description;
                        order.OrderType = command.OrderType;
                        //اگر سفارش برداشت از کیف پول بود
                        if (command.TransactionType == Domain.Enums.WalletType.withdraw)
                        {
                            //برداشت سکه‌ها از کیف پول
                            order.Sign = '-';
                        }
                        //اگر سفارش واریز به کیف پول بود
                        //واریز سکه به کیف پول
                        order.Sign = '+';
                        await _mediator.Send(order);
                        #endregion

                        #region پرداخت

                        #endregion

                        dbContextTransaction.Commit();

                        return 1;
                    }
                    catch (Exception err)
                    {
                        dbContextTransaction.Rollback();
                        throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                    }
                }

            }
        }
    }
}
