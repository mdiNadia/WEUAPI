namespace Domain.Enums
{
    public enum WalletType
    {
        //1  واریز به کیف پول
        //2  برداشت از کیف پول
        //3  درآمد
        //3  انتقال
        deposit = 0,//واریز
        withdraw = 1,//برداشت
        fair = 2,//درآمد
        transfer = 3//انتقال
    }
}
