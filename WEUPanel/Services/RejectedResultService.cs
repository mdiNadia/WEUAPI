using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.RejectedResult;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class RejectedResultService : IRejectedResultService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public RejectedResultService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }
        public async Task<HttpResponseMessage> AddEntity(RejectedResultModels.CreateRejectedResult command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/RejectedResult", command);
            return result;
        }
        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/RejectedResult", command);
            return result;
        }
        public async Task<PagedResponse<IEnumerable<RejectedResultModels.RejectedResult>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<RejectedResultModels.RejectedResult>>>(_baseRequestParameter._Root_Url + "/RejectedResult" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }
        public async Task<Response<RejectedResultModels.RejectedResult>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<RejectedResultModels.RejectedResult>>(_baseRequestParameter._Root_Url + "/RejectedResult/" + id);
            return result;
        }
    }
}
