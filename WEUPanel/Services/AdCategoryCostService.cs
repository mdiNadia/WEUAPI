using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.AdCategoryCost;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class AdCategoryCostService : IAdCategoryCostService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public AdCategoryCostService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndId>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/AdCategoryCost/GetCities");
            return result;
        }

        public async Task<PagedResponse<IEnumerable<AdCategoryCostModels.AdcategoryCost>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<AdCategoryCostModels.AdcategoryCost>>>(_baseRequestParameter._Root_Url + "/AdCategoryCost" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(AdCategoryCostModels.CreateAdcategoryCost command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/AdCategoryCost", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, AdCategoryCostModels.EditAdcategoryCost command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/AdCategoryCost" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<AdCategoryCostModels.AdcategoryCost>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<AdCategoryCostModels.AdcategoryCost>>(_baseRequestParameter._Root_Url + "/AdCategoryCost/" + id);
            return result;
        }
        public async Task<Response<AdCategoryCostModels.AdcategoryCost>> GetByAdCategoryId(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<AdCategoryCostModels.AdcategoryCost>>(_baseRequestParameter._Root_Url + "/AdCategoryCost/GetByCategoryId?id=" + id);
            return result;
        }
        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/AdCategoryCost/" + id);
            return result;
        }
        public async Task<HttpResponseMessage> HandleCost(int id)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/AdCategoryCost/HandleCost?id=" + id, null);
            return result;
        }
        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/AdCategoryCost", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/AdCategoryCost" + "/Update?id=" + id, command);
            return result;
        }

    }
}
