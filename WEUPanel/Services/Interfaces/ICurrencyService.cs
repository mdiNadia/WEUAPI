using WEUPanel.Pages.Currency;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<CurrencyModels.Currency>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<CurrencyModels.Currency>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(CurrencyModels.CreateCurrency command);
        Task<HttpResponseMessage> UpdateEntity(int id, CurrencyModels.EditCurrency command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);

    }
}
