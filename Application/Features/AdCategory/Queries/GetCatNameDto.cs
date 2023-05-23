namespace Application.Features.AdCategory.Queries
{
    public class GetCatNameDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Cost { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
