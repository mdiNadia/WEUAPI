using Application.Errors;
using Application.Features.Order.Commands;
using Application.Features.Transaction.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Value.Commands
{
    public class TransferValue : IRequest<bool>
    {
        public int Value { get; set; }
        public string TargetNumber { get; set; }
        public class TransferValueHandler : IRequestHandler<TransferValue, bool>
        {
            private readonly IMediator _mediator;
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public TransferValueHandler(IMediator mediator, IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;

            }

            public async Task<bool> Handle(TransferValue command, CancellationToken cancellationToken)
            {
                using (var dbContextTransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        #region Users Info
                        var observer = await _unitOfWork.Profiles.GetQueryList()
                  .SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                        if (observer == null)
                            throw new RestException(HttpStatusCode.NotFound, "Not found User 1");

                        var target = await _unitOfWork.Users.GetQueryList().SingleOrDefaultAsync(x => x.PhoneNumber == command.TargetNumber);
                        if (target == null)
                            throw new RestException(HttpStatusCode.NotFound, "Not found User 2");
                        var targetProfile = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == target.UserName);
                        if (targetProfile == null)
                            throw new RestException(HttpStatusCode.NotFound, "Not found User 2 Profile");
                        var observerWallet = await _unitOfWork.Wallets.GetQueryList().SingleOrDefaultAsync(x => x.ProfileId == observer.Id);
                        if (targetProfile == null)
                            throw new RestException(HttpStatusCode.NotFound, "Not found User 1 Wallet");
                        var targetWallet = await _unitOfWork.Wallets.GetQueryList().SingleOrDefaultAsync(x => x.ProfileId == targetProfile.Id);
                        if (targetProfile == null)
                            throw new RestException(HttpStatusCode.NotFound, "Not found User 2 Wallet");
                        #endregion

                        #region ثبت انتقال در دیتابیس
                        var TransferValue = new Domain.Entities.TransferValueHistory
                        {
                            Observer = observer,
                            Target = targetProfile,
                        };
                        TransferValue.CreationDate = DateTime.Now;
                        _unitOfWork.TransferValueHistories.Insert(TransferValue);
                        #endregion
                        #region Exception Handling
                        var observeUserNum = await _unitOfWork.Users.GetQueryList().SingleAsync(c => c.UserName == _userAccessor.GetCurrentUserNameAsync());
                        if (observeUserNum.PhoneNumber == command.TargetNumber)
                            throw new RestException(HttpStatusCode.NotFound, "You can not transfer value to yourself");
                        if (observerWallet.Value < command.Value)
                            throw new RestException(HttpStatusCode.NotFound, "You have not enough value to transfer");

                        #endregion

                        #region کم کردن مقدار انتقال از کیف پول انتقال‌دهنده
                        observerWallet.Value = observerWallet.Value - command.Value;
                        _unitOfWork.Wallets.Update(observerWallet);
                        #endregion
                        #region ایجاد سفارش برای انتقال‌دهنده
                        CreateOrderRow observerOrder = new CreateOrderRow();
                        observerOrder.OrderType = Domain.Enums.OrderType.transfer;
                        observerOrder.Description = $"بابت انتقال آگهی";
                        observerOrder.TargetId = targetProfile.Id;
                        observerOrder.Sign = '-';
                        //طرفی که بهش انتقال میدیم
                        observerOrder.Name = targetProfile.Username;
                        await _mediator.Send(observerOrder);
                        #endregion


                        #region اضافه کردن مقدار انتقال به کیف پول هدف
                        targetWallet.Value = targetWallet.Value + command.Value;
                        _unitOfWork.Wallets.Update(observerWallet);
                        #endregion
                        #region ایجاد سفارش برای کاربر هدف
                        CreateOrderRow targetOrder = new CreateOrderRow();
                        targetOrder.OrderType = Domain.Enums.OrderType.transfer;
                        targetOrder.Description = $"بابت انتقال آگهی";
                        targetOrder.TargetId = observer.Id; // observer ID
                        targetOrder.Sign = '+';
                        //طرفی که بهش انتقال داده شده
                        targetOrder.Name = observer.Username; // observer userName
                        await _mediator.Send(targetOrder);
                        #endregion

                        await _unitOfWork.CompleteAsync();
                        dbContextTransaction.Commit();
                        return true;


                    }
                    catch (Exception)
                    {

                        dbContextTransaction.Rollback();
                        throw new Exception("خطا در ذخیره اطلاعات!");
                    }

                }
            }
        }
    }
}
