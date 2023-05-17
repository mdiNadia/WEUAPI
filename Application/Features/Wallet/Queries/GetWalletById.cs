using Application.Dtos.Common;
using Application.Dtos.Wallet;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Wallet.Queries
{
    public class GetWalletById : IRequest<GetWalletDto>
    {
        public int Id { get; set; }
        public class GetWalletByIdHandler : IRequestHandler<GetWalletById, GetWalletDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetWalletByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetWalletDto> Handle(GetWalletById query, CancellationToken cancellationToken)
            {

                var wallet = await _unitOfWork.Wallets.GetQueryList()
                    .Include(c => c.Profile)
                    .AsNoTracking().Where(c => c.Id == query.Id).Select(c => new GetWalletDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        IsActive = c.IsActive,
                        UpdateDate = c.UpdateDate,
                        CreationDate = c.CreationDate,
                        TotalCredit = c.TotalCredit,
                        Currency = new GetNameAndId()
                        {
                            Id = c.Currency.Id,
                            Name = c.Currency.CurrencyName,
                        },
                        User = new GetNameAndId()
                        {
                            Id = c.Profile.Id,
                            Name = c.Profile.Username,
                        }
                    }).SingleOrDefaultAsync();
                if (wallet == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return wallet;


            }
        }
    }
}
