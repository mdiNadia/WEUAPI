using WEUPanel.Pages.ReportsAds;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IReportedService
    {

        Task<PagedResponse<IEnumerable<ReportedModels.Reported>>> GetAllReportedAdsByPaging(int pageIndex, int pageSize);
        Task<PagedResponse<IEnumerable<ReportedModels.Reported>>> GetAllReportedUsersByPaging(int pageIndex, int pageSize);
        Task<PagedResponse<IEnumerable<ReportedModels.BlackList>>> GetBlackListByPaging(int pageIndex, int pageSize);

    }
}
