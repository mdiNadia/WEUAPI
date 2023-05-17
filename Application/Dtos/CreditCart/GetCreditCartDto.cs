using Application.Dtos.Common;

namespace Application.Dtos.CreditCart
{
    public class GetCreditCartDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardShebaNumber { get; set; }
        public DateTime Expiredate { get; set; }
        public bool IsDeleted { get; set; }
        public GetNameAndId Wallet { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
