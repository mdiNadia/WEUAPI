using Domain.Entities;


namespace Application.Interfaces
{
    public interface IReportReasonRepository : IGenericRepository<ReportReason>
    {
        Task<bool> CheckIfHasChildren(int reasonId);
    }
}
