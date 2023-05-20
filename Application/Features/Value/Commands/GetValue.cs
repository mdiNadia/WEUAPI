using Application.Errors;
using Application.Features.Order.Commands;
using Application.Features.Transaction.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

//درآمد - کسب سکه از طریق دیدن آگهی
namespace Application.Features.Value.Commands
{
    public class GetValue : IRequest<bool>
    {

        public int ValuePerVisit { get; set; }  
        public int TargetId { get;set; }
        public string TargetName { get; set; }
        public class GetValueHandler : IRequestHandler<GetValue, bool>
        {
            private readonly IMediator _mediator;
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public GetValueHandler(IMediator mediator, IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(GetValue command, CancellationToken cancellationToken)
            {
                using (var dbContextTransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        #region Users Info
                        var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                        if (observer == null)
                            throw new RestException(HttpStatusCode.NotFound, "Not found User");
                        var observerWallet = await _unitOfWork.Wallets.GetQueryList().SingleOrDefaultAsync(x => x.ProfileId == observer.Id);
                        if (observerWallet == null)
                            throw new RestException(HttpStatusCode.NotFound, "Not found wallet");
                        #endregion 
                        #region اضافه کردن ارزش به کیف پول
                        observerWallet.Value = observerWallet.Value + command.ValuePerVisit;
                        _unitOfWork.Wallets.Update(observerWallet);
                        #endregion
                        #region ایجاد یک سفارش
                        CreateOrderRow order = new CreateOrderRow();
                        order.OrderType = Domain.Enums.OrderType.fair;
                        order.Name = command.TargetName;//Advertise Title
                        order.Description = $"{command.TargetName} بابت دیدن آگهی";
                        order.TargetId = command.TargetId; //Advertise Id
                        order.Sign = '+';
                        await _mediator.Send(order);
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
