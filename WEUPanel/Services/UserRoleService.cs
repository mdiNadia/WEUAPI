using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.UserRole;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class UserRoleService : IUserRoleService
    {
        public HttpClient _HttpClient { get; }
        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public UserRoleService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndIdString>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndIdString>>(_baseRequestParameter._Root_Url + "/Role/GetRoles");
            return result;
        }
        public async Task<PagedResponse<IEnumerable<UserRoleModels.Role>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<UserRoleModels.Role>>>(_baseRequestParameter._Root_Url + "/Role" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(UserRoleModels.CreateRole command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/Role", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, UserRoleModels.EditRole command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/Role" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<UserRoleModels.Role>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<UserRoleModels.Role>>(_baseRequestParameter._Root_Url + "/Role/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/Role/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/Role", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/Role" + "/Update?id=" + id, command);
            return result;
        }


    }
}
