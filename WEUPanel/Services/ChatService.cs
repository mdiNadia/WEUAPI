using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.Message;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Services
{
    public class ChatService : IChatService
    {
        public HttpClient _HttpClient { get; }

        private readonly NavigationManager _navigationManager;
        private readonly BaseRequestParameter _baseRequestParameter;

        public ChatService(NavigationManager navigationManager, HttpClient httpClient, BaseRequestParameter baseRequestParameter)
        {
            this._navigationManager = navigationManager;
            this._HttpClient = httpClient;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<PagedResponse<IEnumerable<MessageModels.Message>>> GetAllByPaging(string username, int pageIndex, int pageSize)
        {
            MessageModels.MessageParams messageParams = new MessageModels.MessageParams();
            messageParams.Username = username;
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = await _HttpClient.GetFromJsonAsync<PagedResponse<IEnumerable<MessageModels.Message>>>(_baseRequestParameter._Root_Url + "/Message?username=" + username + "&pageNumber=" + pageIndex + "&pageSize=" + pageSize);
            return result;
        }
    }
}
