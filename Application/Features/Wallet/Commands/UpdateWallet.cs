using Application.Errors;
using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;
using System.Net;

namespace Application.Features.Wallet.Commands
{
    public class UpdateWallet : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        //واحد پول
        public int? CurrencyId { get; set; }
        public class UpdateWalletHandler : IRequestHandler<UpdateWallet, int>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public UpdateWalletHandler(IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }
            public async Task<int> Handle(UpdateWallet command, CancellationToken cancellationToken)
            {

                var wallet = await _unitOfWork.Wallets.GetByID(command.Id);

                if (wallet == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                else
                {
                    wallet.UpdateDate = DateTime.Now;
                    wallet.Name = command.Name;
                    wallet.Description = command.Description;
                    wallet.IsActive = command.IsActive;
                    _unitOfWork.Wallets.Update(wallet);

                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return wallet.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }


            }
        }
    }
}
