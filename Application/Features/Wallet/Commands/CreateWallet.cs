using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;

namespace Application.Features.Wallet.Commands
{
    public class CreateWallet : IRequest<int>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        //واحد پول
        public int? CurrencyId { get; set; }

        public decimal TotalCredit { get; set; } = 0;
        public int ProfileId { get; set; }
        public class CreateWalletHandler : IRequestHandler<CreateWallet, int>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateWalletHandler(IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateWallet command, CancellationToken cancellationToken)
            {

                var wallet = new Domain.Entities.Wallet();
                wallet.Name = command.Name;
                wallet.Description = command.Description;
                wallet.IsActive = command.IsActive;
                //اگر واحد پول فعالی در دیتابیس نباشد این 0 میاد که باعث ارور میشه
                wallet.CurrencyId = command.CurrencyId;
                //
                wallet.CreationDate = DateTime.Now;
                wallet.UpdateDate = DateTime.Now;
                wallet.TotalCredit = command.TotalCredit;
                wallet.ProfileId = command.ProfileId;
                _unitOfWork.Wallets.Insert(wallet);

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
