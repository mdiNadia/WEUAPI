using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.ProfileScore;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class ProfileScoreService : IProfileScoreService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public ProfileScoreService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<HttpResponseMessage> AddEntity(ProfileScoreModels.CreateProfileScore command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/profileScore", command);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/profileScore", command);
            return result;
        }
        public async Task<IEnumerable<ProfileScoreModels.ProfileScore>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<IEnumerable<ProfileScoreModels.ProfileScore>>(_baseRequestParameter._Root_Url + "/profileScore");
            return result;
        }
        public async Task<PagedResponse<IEnumerable<ProfileScoreModels.ProfileScore>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<ProfileScoreModels.ProfileScore>>>(_baseRequestParameter._Root_Url + "/profileScore" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<Response<ProfileScoreModels.ProfileScore>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<ProfileScoreModels.ProfileScore>>(_baseRequestParameter._Root_Url + "/profileScore/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/profileScore/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, ProfileScoreModels.EditProfileScore command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/profileScore" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/profileScore" + "/Update?id=" + id, command);
            return result;
        }
    }
}
