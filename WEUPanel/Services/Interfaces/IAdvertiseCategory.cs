using WEUPanel.Pages.AdvertiseCategory;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IAdvertiseCategory
    {
        Task<List<AdvertiseCategoryModels.GetCatNameDto>> GetAll();
        Task<PagedResponse<IEnumerable<AdvertiseCategoryModels.AdvertiseCategory>>> GetAllByPaging(int pageIndex, int pageSize);

        Task<Response<AdvertiseCategoryModels.AdvertiseCategory>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(AdvertiseCategoryModels.CreateAdvertiseCategory command);
        Task<HttpResponseMessage> UpdateEntity(int id, AdvertiseCategoryModels.EditAdvertiseCategory command);

        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);
    }
}
