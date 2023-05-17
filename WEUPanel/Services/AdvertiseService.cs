using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.Advertisement;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class AdvertiseService : IAdveriseService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public AdvertiseService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<IEnumerable<GetNameAndId>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<IEnumerable<GetNameAndId>>(_baseRequestParameter._Root_Url + "/Advertising/GetAdvertisings");
            return result;
        }

        public async Task<PagedResponse<IEnumerable<AdvertisementModels.Advertisement>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<AdvertisementModels.Advertisement>>>(_baseRequestParameter._Root_Url + "/Advertising" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(AdvertisementModels.CreateAdvertisement command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/Advertising", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, AdvertisementModels.EditAdvertisement command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/Advertising" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<AdvertisementModels.Advertisement>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<AdvertisementModels.Advertisement>>(_baseRequestParameter._Root_Url + "/Advertising/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/Advertising/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/Advertising", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/Advertising" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateDisplayedField(int id, AdvertisementModels.UpdateDisplayedField command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/Advertising" + "/UpdateDisplayedField?id=" + id, command);
            return result;
        }
        public async Task<HttpResponseMessage> UpdateIsActiveField(int id, AdvertisementModels.UpdateIsActiveField command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/Advertising" + "/UpdateIsActiveField?id=" + id, command);
            return result;
        }
    }
}
