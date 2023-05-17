using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.User;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class UserService : IUserService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public UserService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndIdString>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndIdString>>(_baseRequestParameter._Root_Url + "/user/GetUsers");
            return result;
        }

        public async Task<int> GetAllCount()
        {

            var result = await _HttpClient.GetFromJsonAsync<int>(_baseRequestParameter._Root_Url + "/user/GetAllCount");
            return result;
        }
        public async Task<PagedResponse<IEnumerable<UserModels.User>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<UserModels.User>>>(_baseRequestParameter._Root_Url + "/user" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(UserModels.CreateUser command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/user", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(string id, UserModels.EditUser command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/user" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<UserModels.User>> GetById(string id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<UserModels.User>>(_baseRequestParameter._Root_Url + "/user/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(string id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/user/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/user", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(string id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/user" + "/Update?id=" + id, command);
            return result;
        }
    }
}
