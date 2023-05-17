using WEUPanel.Wrappers;

namespace WEUPanel.Pages.Transaction
{
    public class TransactionModels
    {
        public class Transaction
        {
            public int Id { get; init; }
            public string Description { get; init; }
            public DateTime CreationDate { get; init; }
            public decimal Amount { get; init; }
            public bool IsDeleted { get; init; }
            public GetNameAndId Wallet { get; init; }
            public GetNameAndId TransactionType { get; init; }
        }
    }
}
