using Application.Dtos.Common;

namespace Application.Features.Wallet.Queries
{
    public record GetWalletDto
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public bool IsActive { get; init; }
        //واحد پول
        public GetNameAndId Currency { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime UpdateDate { get; init; }

        public decimal TotalCredit { get; init; }

        public int Value { get; set; }
        public GetNameAndId User { get; init; }
    }


}
