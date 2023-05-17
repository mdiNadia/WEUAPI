using WEUPanel.Pages.RejectedResult;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IRejectedResultService
    {
        Task<PagedResponse<IEnumerable<RejectedResultModels.RejectedResult>>> GetAllByPaging(int pageIndex, int pageSize);

        Task<HttpResponseMessage> AddEntity(RejectedResultModels.CreateRejectedResult command);
        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<Response<RejectedResultModels.RejectedResult>> GetById(int id);
    }
}
