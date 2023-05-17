namespace WEUPanel.Pages.Country
{
    public class CountryModels
    {
        public class Country
        {
            public int Id { get; set; }
            public string Iso { get; set; }
            public string Name { get; set; }
            public string? Iso3 { get; set; }
            public int? NumCode { get; set; }
            public int? PhoneCode { get; set; }
            public int? CurrencyId { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreationDate { get; set; }

        }
        public class CreateCountry
        {
            public string Iso { get; set; }
            public string Name { get; set; }
            public string? Iso3 { get; set; }
            public int? NumCode { get; set; }
            public int? PhoneCode { get; set; }
            public bool IsActive { get; set; }

        }
        public class EditCountry
        {
            public int Id { get; set; }
            public string Iso { get; set; }
            public string Name { get; set; }
            public string? Iso3 { get; set; }
            public int? NumCode { get; set; }
            public int? PhoneCode { get; set; }
            public bool IsActive { get; set; }

        }
    }
}
