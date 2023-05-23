using Application.Dtos.Common;

namespace Application.Features.ReportReason.Queries
{
    public record GetReportReasonDto
    {
        public int Id { get; init; }
        public int ReportReasonType { get; init; }
        public GetNameAndId Parent { get; init; }
        public string Reason { get; init; }
        public DateTime CreationDate { get; init; }

    }

}
