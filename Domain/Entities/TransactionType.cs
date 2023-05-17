using Domain.Common;

namespace Domain.Entities
{
    //1  واریز سکه با پول
    //2  برداشت سکه بصورت پول
    //3  درآمد - کسب سکه از دیدن آگهی
    //4  انتقال
    public class TransactionType : BaseEntity
    {

        public Enums.WalletType TransactionTypeEnum { get; set; }


        //رابطه
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }


    }
}
