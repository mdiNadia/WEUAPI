using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Wallet.Commands
{
    public class DeleteWalletById : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteWalletByIdHandler : IRequestHandler<DeleteWalletById, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteWalletByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(DeleteWalletById command, CancellationToken cancellationToken)
            {

                var wallet = await _unitOfWork.Wallets.GetByID(command.Id);
                if (wallet == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                _unitOfWork.Wallets.Delete(wallet);
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
