using WEUPanel.Pages.Advertisement;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IAdveriseService
    {
        Task<IEnumerable<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<AdvertisementModels.Advertisement>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<AdvertisementModels.Advertisement>> GetById(int id);
        Task<HttpResponseMessage> UpdateDisplayedField(int id, AdvertisementModels.UpdateDisplayedField command);

        Task<HttpResponseMessage> UpdateIsActiveField(int id, AdvertisementModels.UpdateIsActiveField command);
        Task<HttpResponseMessage> AddEntity(AdvertisementModels.CreateAdvertisement command);
        Task<HttpResponseMessage> UpdateEntity(int id, AdvertisementModels.EditAdvertisement command);
        Task<HttpResponseMessage> RemoveEntity(int id);
        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);

    }
}
