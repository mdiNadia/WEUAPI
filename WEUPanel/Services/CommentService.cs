using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.Comment;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class CommentService : ICommentService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public CommentService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndId>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/comment/Getcomments");
            return result;
        }

        public async Task<PagedResponse<IEnumerable<CommentModels.Comment>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<CommentModels.Comment>>>(_baseRequestParameter._Root_Url + "/comment" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(CommentModels.CreateComment command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/comment", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, CommentModels.EditComment command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/comment" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<CommentModels.Comment>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<CommentModels.Comment>>(_baseRequestParameter._Root_Url + "/comment/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/comment/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/comment", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/comment" + "/Update?id=" + id, command);
            return result;
        }

    }
}
