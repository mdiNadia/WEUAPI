using Application.Interfaces;
using MediatR;

namespace Application.Features.CreditCart.Commands
{
    public class DeleteCreditCartById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteCreditCartByIdHandler : IRequestHandler<DeleteCreditCartById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCreditCartByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteCreditCartById command, CancellationToken cancellationToken)
            {

                //حذف منطقی
                var creditCart = await _unitOfWork.BankAccounts.GetByID(command.Id);
                creditCart.IsDeleted = true;
                _unitOfWork.BankAccounts.Update(creditCart);

                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{creditCart.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }




            }
        }
    }
}
