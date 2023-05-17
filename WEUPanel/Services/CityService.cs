using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.City;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class CityService : ICityService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public CityService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndId>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/City/GetCities");
            return result;
        }
        public async Task<List<GetNameAndId>> GetAllWithoutPaging()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/City/GetAllCities");
            return result;
        }
        public async Task<PagedResponse<IEnumerable<CityModels.City>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<CityModels.City>>>(_baseRequestParameter._Root_Url + "/City" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(CityModels.CreateCity command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/City", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, CityModels.EditCity command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/City" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<CityModels.City>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<CityModels.City>>(_baseRequestParameter._Root_Url + "/City/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/City/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/City", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/City" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<List<GetNameAndId>> GetAllByProvinceIds(List<int> ids)
        {
            var ser = JsonSerializer.Serialize(ids);
            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/City/GetCities?ids=" + ser);
            return result;
        }

    }
}
