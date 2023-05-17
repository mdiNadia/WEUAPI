using WEUPanel.Pages.AdCategoryCost;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IAdCategoryCostService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<AdCategoryCostModels.AdcategoryCost>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<AdCategoryCostModels.AdcategoryCost>> GetById(int id);
        Task<Response<AdCategoryCostModels.AdcategoryCost>> GetByAdCategoryId(int id);

        Task<HttpResponseMessage> AddEntity(AdCategoryCostModels.CreateAdcategoryCost command);
        Task<HttpResponseMessage> UpdateEntity(int id, AdCategoryCostModels.EditAdcategoryCost command);
        Task<HttpResponseMessage> RemoveEntity(int id);
        Task<HttpResponseMessage> HandleCost(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);
    }
}
