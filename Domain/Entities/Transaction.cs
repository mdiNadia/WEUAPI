using Domain.Common;

namespace Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get; set; }

        public decimal Amount { get; set; }
        public char TransactionSign { get;set; }
        //رابطه
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int TransactionStatusId { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
