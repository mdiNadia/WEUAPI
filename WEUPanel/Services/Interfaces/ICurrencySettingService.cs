using WEUPanel.Pages.CurrencySetting;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface ICurrencySettingService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<CurrencySettingModels.CurrencySetting>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<PagedResponse<IEnumerable<CurrencySettingModels.CurrencySetting>>> GetAllCurrencySettingsByCurrencyId(int id, int pageIndex, int pageSize);
        Task<Response<CurrencySettingModels.CurrencySetting>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(CurrencySettingModels.CreateCurrencySetting command);
        Task<HttpResponseMessage> UpdateEntity(int id, CurrencySettingModels.EditCurrencySetting command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);

    }
}
