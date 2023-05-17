using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Transaction.Commands
{
    public class DeleteTransactionById : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteTransactionByIdHandler : IRequestHandler<DeleteTransactionById, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteTransactionByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(DeleteTransactionById command, CancellationToken cancellationToken)
            {
                var transaction = await _unitOfWork.Transactions.GetByID(command.Id);
                if (transaction == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                _unitOfWork.Transactions.Update(transaction);
                try
                {
                    await _unitOfWork.CompleteAsync();

                    return command.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
