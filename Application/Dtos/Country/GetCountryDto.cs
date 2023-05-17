namespace Application.Dtos.Country
{
    public record GetCountryDto
    {
        public int Id { get; init; }
        public string Iso { get; init; }
        public string Name { get; init; }
        public string? Iso3 { get; init; }
        public int? NumCode { get; init; }
        public int? PhoneCode { get; init; }
        public bool IsActive { get; set; }
        public int? CurrencyId { get; init; }
        public DateTime CreationDate { get; init; }

    }

}
