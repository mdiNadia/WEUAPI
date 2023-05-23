namespace Application.Features.CurrencySetting.Queries
{
    public class GetCurrencySettingDto
    {
        public int Id { get; set; }
        public decimal Buy { get; set; }
        //نرخ فروش
        public decimal Sale { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int CurrencyId { get; set; }
    }
}
