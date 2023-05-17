using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;

using WEUPanel.Pages.FileType;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class FileTypeService : IFileTypeService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public FileTypeService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndId>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/FileType");
            return result;
        }

        public async Task<PagedResponse<IEnumerable<FileTypeModels.FileType>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<FileTypeModels.FileType>>>(_baseRequestParameter._Root_Url + "/FileType" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }
        public async Task<HttpResponseMessage> AddEntity(FileTypeModels.CreateFileType command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/FileType", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, FileTypeModels.EditFileType command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/FileType" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<FileTypeModels.FileType>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<FileTypeModels.FileType>>(_baseRequestParameter._Root_Url + "/FileType/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/FileType/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/FileType", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/FileType" + "/Update?id=" + id, command);
            return result;
        }
    }
}
