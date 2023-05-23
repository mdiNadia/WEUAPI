using Application.Dtos.Common;

namespace Application.Features.Boost.Queries
{
    public class GetBoostDto
    {
        public int NumberOfadViews { get; set; }
        public decimal ValuePerVisit { get; set; }
        public decimal Debit { get; set; }
        public DateTime CreationDate { get; set; }
        public GetNameAndId Advertising { get; set; }
    }
}
