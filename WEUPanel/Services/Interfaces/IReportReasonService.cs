using WEUPanel.Pages.ReportAdsReason;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IReportReasonService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<ReportReasonModels.ReportReason>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<ReportReasonModels.ReportReason>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(ReportReasonModels.CreateReportReason command);
        Task<HttpResponseMessage> UpdateEntity(int id, ReportReasonModels.EditReportReason command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);
    }
}
