using Domain.Common;

namespace Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        //واحد پول
        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public decimal TotalCredit { get; set; }
        public int Value { get; set; } = 0;

        //رابطه
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
