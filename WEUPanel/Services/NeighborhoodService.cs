using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.Neighborhood;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class NeighborhoodService : INeighborhoodService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public NeighborhoodService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndId>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/Neighborhood/GetNeighborhoods");
            return result;
        }
        public async Task<List<GetNameAndId>> GetAllWithoutPaging()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/Neighborhood/GetAllNeighborhoods");
            return result;
        }
        public async Task<PagedResponse<IEnumerable<NeighborhoodModels.Neighborhood>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<NeighborhoodModels.Neighborhood>>>(_baseRequestParameter._Root_Url + "/Neighborhood" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(NeighborhoodModels.CreateNeighborhood command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/Neighborhood", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, NeighborhoodModels.EditNeighborhood command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/Neighborhood" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<NeighborhoodModels.Neighborhood>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<NeighborhoodModels.Neighborhood>>(_baseRequestParameter._Root_Url + "/Neighborhood/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/Neighborhood/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/Neighborhood", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/Neighborhood" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<List<GetNameAndId>> GetAllByCityIds(List<int> ids)
        {
            var ser = JsonSerializer.Serialize(ids);
            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/Neighborhood/GetNeighborhoods?ids=" + ser);
            return result;
        }
    }
}
