namespace Application.Features.Currency.Queries
{
    public record GetCurrencyDto
    {
        public int Id { get; init; }
        public string CurrencyName { get; init; }
        public bool IsDefault { get; init; }
        public bool IsActive { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
