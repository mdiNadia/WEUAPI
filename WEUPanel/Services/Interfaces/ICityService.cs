using WEUPanel.Pages.City;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface ICityService
    {
        Task<List<GetNameAndId>> GetAll();
        Task<List<GetNameAndId>> GetAllWithoutPaging();
        Task<List<GetNameAndId>> GetAllByProvinceIds(List<int> ids);

        Task<PagedResponse<IEnumerable<CityModels.City>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<CityModels.City>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(CityModels.CreateCity command);
        Task<HttpResponseMessage> UpdateEntity(int id, CityModels.EditCity command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);

    }
}
