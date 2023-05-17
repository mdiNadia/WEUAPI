using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.CreditCart.Commands
{
    public class UpdateCreditCart : IRequest<int>
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardShebaNumber { get; set; }
        public DateTime Expiredate { get; set; }

        public class UpdateCreditCartHandler : IRequestHandler<UpdateCreditCart, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateCreditCartHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateCreditCart command, CancellationToken cancellationToken)
            {

                var creditCart = await _unitOfWork.BankAccounts.GetByID(command.Id);

                if (creditCart == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                else
                {
                    creditCart.CardName = creditCart.CardName;
                    creditCart.CardNumber = creditCart.CardNumber;
                    creditCart.CardShebaNumber = creditCart.CardShebaNumber;
                    creditCart.Expiredate = creditCart.Expiredate;
                    _unitOfWork.BankAccounts.Update(creditCart);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return creditCart.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
                }


            }
        }
    }
}
