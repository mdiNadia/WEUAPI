using WEUPanel.Pages.ConfirmedResult;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IConfirmedResultservice
    {
        Task<PagedResponse<IEnumerable<ConfirmedResultModels.ConfirmedResult>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<HttpResponseMessage> AddEntity(ConfirmedResultModels.CreateConfirmedResult command);
        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<Response<ConfirmedResultModels.ConfirmedResult>> GetById(int id);
    }
}
