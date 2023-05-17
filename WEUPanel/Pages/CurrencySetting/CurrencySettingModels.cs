namespace WEUPanel.Pages.CurrencySetting
{
    public class CurrencySettingModels
    {
        public class CurrencySetting
        {
            public int Id { get; set; }
            public decimal Buy { get; set; }
            //نرخ فروش
            public decimal Sale { get; set; }

            public DateTime CreationDate { get; set; }
            public DateTime UpdatedDate { get; set; }

            public int CurrencyId { get; set; }
        }
        public class CreateCurrencySetting
        {
            public decimal Buy { get; set; }
            public decimal Sale { get; set; }
            public int CurrencyId { get; set; }
        }
        public class EditCurrencySetting
        {
            public int Id { get; set; }
            public decimal Buy { get; set; }
            public decimal Sale { get; set; }
        }
    }
}
