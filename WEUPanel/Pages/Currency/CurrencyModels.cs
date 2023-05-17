namespace WEUPanel.Pages.Currency
{
    public class CurrencyModels
    {
        public class Currency
        {
            public int Id { get; set; }
            public int CountryId { get; set; }
            public string CurrencyName { get; set; }
            public bool IsDefault { get; set; }
            public DateTime CreationDate { get; set; }

        }
        public class CreateCurrency
        {
            public int Id { get; set; } = 0;
            public int CountryId { get; set; }
            public string CurrencyName { get; set; }
            public bool IsDefault { get; set; }
        }
        public class EditCurrency
        {
            public int Id { get; set; }
            public string CurrencyName { get; set; }
            public bool IsDefault { get; set; }
        }
    }
}
