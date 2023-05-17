using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WEUPanel.Helpers;
using WEUPanel.Pages.Account;

namespace WEUPanel.Services.Account
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly BaseRequestParameter _baseRequestParameter;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage,
                           BaseRequestParameter baseRequestParameter)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            this._baseRequestParameter = baseRequestParameter;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseRequestParameter._Root_Url + "/UserAccessor/Register", registerModel);
            var registerResult = JsonSerializer.Deserialize<RegisterResult>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return registerResult;
        }

        public async Task<AuthenticationModel> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync(_baseRequestParameter._Root_Url + "/UserAccessor/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<AuthenticationModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //loginResult.statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }


            await _localStorage.SetItemAsync("token", loginResult.Token);
            await _localStorage.SetItemAsync("name", loginResult.UserName);
            //await _localStorage.SetItemAsync("expire", DateTime.Now.AddMinutes(60));
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.username);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            await _localStorage.RemoveItemAsync("name");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;

        }

    }
}
