﻿using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.Province;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class ProvinceService : IProvinceService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public ProvinceService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<List<GetNameAndId>> GetAll()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/Province/GetProvinces");
            return result;
        }
        public async Task<List<GetNameAndId>> GetAllWithoutPaging()
        {

            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/Province/GetAllProvinces");
            return result;
        }
        public async Task<PagedResponse<IEnumerable<ProvinceModels.Province>>> GetAllByPaging(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<ProvinceModels.Province>>>(_baseRequestParameter._Root_Url + "/Province" + "?pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntity(ProvinceModels.CreateProvince command)
        {
            var result = await _HttpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/Province", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, ProvinceModels.EditProvince command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/Province" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<ProvinceModels.Province>> GetById(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<ProvinceModels.Province>>(_baseRequestParameter._Root_Url + "/Province/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> RemoveEntity(int id)
        {
            var result = await _HttpClient.DeleteAsync(_baseRequestParameter._Root_Url + "/Province/" + id);
            return result;
        }

        public async Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command)
        {
            var result = await _HttpClient.PostAsync(_baseRequestParameter._Root_Url + "/Province", command);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command)
        {
            var result = await _HttpClient.PutAsync(_baseRequestParameter._Root_Url + "/Province" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<List<GetNameAndId>> GetAllByCountryId(int id)
        {
            var result = await _HttpClient.GetFromJsonAsync<List<GetNameAndId>>(_baseRequestParameter._Root_Url + "/Province/GetProvinces/" + id);
            return result;
        }
    }
}
