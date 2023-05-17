using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.Language;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;


namespace WEUPanel.Services
{
    public class LanguageService : ILanguageService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public LanguageService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndIdString>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndIdString>>(_baseRequestParameter._Root_Url + "/language/GetLanguages");
            return result;
        }

        public async Task<PagedResponse<IEnumerable<LanguageModels.Language>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<LanguageModels.Language>>>(_baseRequestParameter._Root_Url + "/language" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(LanguageModels.CreateLanguage command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/Language", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, LanguageModels.EditLanguage command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/Language" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<LanguageModels.Language>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<LanguageModels.Language>>(_baseRequestParameter._Root_Url + "/language/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/language/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/Language", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/Language" + "/Update?id=" + id, command);
            return result;
        }

    }
}
