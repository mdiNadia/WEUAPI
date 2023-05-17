using Domain.Common;

namespace Domain.Entities
{
    public class BankAccount : BaseEntity
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardShebaNumber { get; set; }
        public DateTime Expiredate { get; set; }
        public bool IsDeleted { get; set; }
    

        //رابطه
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }


    }
}
