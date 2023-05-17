using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.AppSetting;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class AppSettingService : IAppSettingService
    {
        public HttpClient _HttpClient { get; }
        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public AppSettingService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<HttpResponseMessage> UpdateEntity(int id, AppSettingModels.EditAppSetting command)
        {
            var result = await _HttpClient.PutAsJsonAsync(_baseRequestParameter._Root_Url + "/AppSetting" + "/Update?id=" + id, command);
            return result;
        }

        public async Task<Response<AppSettingModels.AppSetting>> GetAppSetting()
        {
            var result = await _HttpClient.GetFromJsonAsync<Response<AppSettingModels.AppSetting>>(_baseRequestParameter._Root_Url + "/AppSetting");
            return result;
        }

    }
}
