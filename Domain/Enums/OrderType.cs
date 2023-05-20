namespace Domain.Enums
{
    public enum OrderType
    {
        //0  واریز به کیف پول
        //1  برداشت از کیف پول
        //2  درآمد
        //3  انتقال
        //4 فروشگاه
        deposit = 0,//واریز
        withdraw = 1,//برداشت
        fair = 2,//درآمد
        transfer = 3,//انتقال
        shopping = 4, // فروشگاه
    }
}
