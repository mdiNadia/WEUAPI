using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.CurrencySetting;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class CurrencySettingService : ICurrencySettingService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public CurrencySettingService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndId>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/CurrencySetting");
            return result;
        }

        public async Task<PagedResponse<IEnumerable<CurrencySettingModels.CurrencySetting>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<CurrencySettingModels.CurrencySetting>>>(_baseRequestParameter._Root_Url + "/CurrencySetting" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(CurrencySettingModels.CreateCurrencySetting command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/CurrencySetting", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, CurrencySettingModels.EditCurrencySetting command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/CurrencySetting" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<CurrencySettingModels.CurrencySetting>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<CurrencySettingModels.CurrencySetting>>(_baseRequestParameter._Root_Url + "/CurrencySetting/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/CurrencySetting/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/CurrencySetting", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/CurrencySetting" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<PagedResponse<IEnumerable<CurrencySettingModels.CurrencySetting>>> GetAllCurrencySettingsByCurrencyId(int id, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<CurrencySettingModels.CurrencySetting>>>(_baseRequestParameter._Root_Url + "/CurrencySetting" + "/GetAllCurrencySettingsByCurrencyId/" + id + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }
    }
}
