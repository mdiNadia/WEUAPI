using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.CreditCart.Commands
{
    public class CreateCreditCart : IRequest<int>
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardShebaNumber { get; set; }
        public DateTime Expiredate { get; set; }
        public class CreateCreditCartHandler : IRequestHandler<CreateCreditCart, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateCreditCartHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateCreditCart command, CancellationToken cancellationToken)
            {

                BankAccount creditCard = new BankAccount();
                creditCard.CardName = command.CardName;
                creditCard.CardNumber = command.CardNumber;
                creditCard.CardShebaNumber = command.CardShebaNumber;
                creditCard.Expiredate = command.Expiredate;
                creditCard.CreationDate = DateTime.Now;
                _unitOfWork.BankAccounts.Insert(creditCard);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return creditCard.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }






            }
        }
    }
}
