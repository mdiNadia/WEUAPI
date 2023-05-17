using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands
{
    public class CreateOrderRow : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TargetId { get; set; }
        public char Sign { get; set; }
        public OrderType OrderType { get; set; }
        public Domain.Enums.WalletType TransactionType { get; set; }
        public class CreateOrderRowHandler : IRequestHandler<CreateOrderRow, int>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateOrderRowHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateOrderRow command, CancellationToken cancellationToken)
            {
                var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                if (observer == null)
                    throw new RestException(HttpStatusCode.BadRequest, "Not Found User");
                var order = new Domain.Entities.Order();
                order = await _unitOfWork.Orders.GetQueryList()
                    .SingleOrDefaultAsync(c => c.ProfileId == observer.Id && c.IsPaid == false);
                var orderRow = new OrderRow();
                var orderItem = new Domain.Entities.OrderRow();
                if (order == null)
                {
                    order = new Domain.Entities.Order();
                    //
                    order.IsPaid = false;
                    order.OrderNum = GenerateVertificationCode.CreateVertificationCode() + 'o';
                    order.Profile = observer;
                    _unitOfWork.Orders.Insert(order);
                    await _unitOfWork.CompleteAsync();
                   
                }
                orderItem.Order = order;
                orderItem.TargetId = command.TargetId;
                orderItem.Name = command.Name;
                orderItem.Description = command.Description;
                orderItem.OrderType = command.OrderType;
                orderItem.TransactionType = command.TransactionType;
                orderItem.sign = command.Sign;
                _unitOfWork.OrderRows.Insert(orderItem);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return 1;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}

