using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Wallet.Queries
{
    public class GetAllCountWallets : IRequest<int>
    {
        public class GetAllCountWalletsHandler : IRequestHandler<GetAllCountWallets, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountWalletsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountWallets query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Wallets.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }

            }
        }
    }
}
