using WEUPanel.Pages.Language;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<List<GetNameAndIdString>> GetAll();

        Task<PagedResponse<IEnumerable<LanguageModels.Language>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<LanguageModels.Language>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(LanguageModels.CreateLanguage command);
        Task<HttpResponseMessage> UpdateEntity(int id, LanguageModels.EditLanguage command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);


    }
}
