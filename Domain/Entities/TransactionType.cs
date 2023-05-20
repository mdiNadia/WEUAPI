using Domain.Common;

namespace Domain.Entities
{
    //0  واریز 
    //1  برداشت 

    public class TransactionType : BaseEntity
    {
        public Enums.TransactionType TransactionTypeEnum { get; set; }
        //رابطه
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }


    }
}
