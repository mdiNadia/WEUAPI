using Application.Dtos.Common;

namespace Application.Dtos.Transaction
{
    public record GetTransactionDto
    {
        public int Id { get; init; }
        public string Description { get; init; }
        public DateTime CreationDate { get; init; }
        public decimal Amount { get; init; }
        public bool IsDeleted { get; init; }
        //رابطه
        public GetNameAndId Wallet { get; init; }
        public GetNameAndId TransactionType { get; init; }
    }

}
