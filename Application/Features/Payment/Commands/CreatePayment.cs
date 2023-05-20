using Application.Errors;
using Application.Features.Transaction.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Payment.Commands
{
    public class Payment : IRequest<int>
    {
        public decimal Amount { get;set; }
        public class PaymentHandler : IRequestHandler<Payment,int>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMediator _mediator;

            public PaymentHandler(IUnitOfWork unitOfWork,IMediator mediator)
            {
                this._unitOfWork = unitOfWork;
                this._mediator = mediator;
            }
            public async Task<int> Handle(Payment command, CancellationToken cancellationToken)
            {

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
