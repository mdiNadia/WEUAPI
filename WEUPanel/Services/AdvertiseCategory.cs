using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.AdvertiseCategory;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class AdvertiseCategory : IAdvertiseCategory
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public AdvertiseCategory(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<AdvertiseCategoryModels.GetCatNameDto>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<AdvertiseCategoryModels.GetCatNameDto>>(_baseRequestParameter._Root_Url + "/AdCategory/GetCategories");
            return result;
        }

        public async Task<PagedResponse<IEnumerable<AdvertiseCategoryModels.AdvertiseCategory>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<AdvertiseCategoryModels.AdvertiseCategory>>>(_baseRequestParameter._Root_Url + "/AdCategory" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(AdvertiseCategoryModels.CreateAdvertiseCategory command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/AdCategory", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, AdvertiseCategoryModels.EditAdvertiseCategory command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/AdCategory" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<AdvertiseCategoryModels.AdvertiseCategory>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<AdvertiseCategoryModels.AdvertiseCategory>>(_baseRequestParameter._Root_Url + "/AdCategory/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/AdCategory/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/AdCategory", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/AdCategory" + "/Update?id=" + id, command);
            return result;
        }
    }
}
