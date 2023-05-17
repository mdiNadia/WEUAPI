using Application.Errors;
using Application.Features.Transaction.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Prng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

//واریز سکه
namespace Application.Features.Value.Commands
{
    public class ChargeValue : IRequest<decimal>
    {
        public int Value{ get; set; }
        public class ChargeValueHandler : IRequestHandler<ChargeValue, decimal>
        {
            private readonly IMediator _mediator;
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public ChargeValueHandler(IMediator mediator,IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;

            }

            public async Task<decimal> Handle(ChargeValue command, CancellationToken cancellationToken)
            {
                var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                if (observer == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found User");
                var appSetting =await _unitOfWork.AppSettings.GetQueryList().FirstOrDefaultAsync();
                if (appSetting == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found Application Setting");
                var defaultPaymentRate = await _unitOfWork.Currencies.GetQueryList()
                    .Include(c=>c.CurrencySettings)
                    .FirstOrDefaultAsync(c => c.IsDefaultPayRate);
                if (defaultPaymentRate == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found Currency Payment");

                var getLastRate = defaultPaymentRate.CurrencySettings.LastOrDefault();
               if(getLastRate == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found Currency Payment");

               
                decimal amount = (((command.Value * appSetting.Value) * getLastRate.Sale)/1000);

                try
                {
                    await _unitOfWork.CompleteAsync();
                    return amount;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
