using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.ConfirmedResult;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class ConfirmedResultservice : IConfirmedResultservice
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public ConfirmedResultservice(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }
        public async Task<HttpResponseMessage> AddEntity(ConfirmedResultModels.CreateConfirmedResult command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/ConfirmedResult", command);
            return result;
        }
        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/ConfirmedResult", command);
            return result;
        }

        public async Task<PagedResponse<IEnumerable<ConfirmedResultModels.ConfirmedResult>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<ConfirmedResultModels.ConfirmedResult>>>(_baseRequestParameter._Root_Url + "/ConfirmedResult" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);

            return result;
        }

        public async Task<Response<ConfirmedResultModels.ConfirmedResult>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<ConfirmedResultModels.ConfirmedResult>>(_baseRequestParameter._Root_Url + "/ConfirmedResult/" + id);
            return result;
        }
    }
}
