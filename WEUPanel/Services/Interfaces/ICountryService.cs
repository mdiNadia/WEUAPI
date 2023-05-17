using WEUPanel.Pages.Country;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<CountryModels.Country>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<CountryModels.Country>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(CountryModels.CreateCountry command);
        Task<HttpResponseMessage> UpdateEntity(int id, CountryModels.EditCountry command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);

    }
}
