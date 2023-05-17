using WEUPanel.Pages.Province;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IProvinceService
    {
        Task<List<GetNameAndId>> GetAll();
        Task<List<GetNameAndId>> GetAllWithoutPaging();
        Task<List<GetNameAndId>> GetAllByCountryId(int id);
        Task<PagedResponse<IEnumerable<ProvinceModels.Province>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<ProvinceModels.Province>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(ProvinceModels.CreateProvince command);
        Task<HttpResponseMessage> UpdateEntity(int id, ProvinceModels.EditProvince command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);

    }
}
