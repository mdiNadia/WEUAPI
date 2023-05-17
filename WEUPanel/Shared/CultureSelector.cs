
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using WEUPanel.Services.Interfaces;
using WEUPanel.Wrappers;

namespace WEUPanel.Shared
{
    public partial class CultureSelector
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public List<GetNameAndIdString> Languages { get; set; } =  new List<GetNameAndIdString>();
        public List<CultureInfo> cultures { get; set; } = new List<CultureInfo>();
        string CurrentCulture = CultureInfo.CurrentCulture.DisplayName;
        public async Task StartCountdown(bool flag, List<GetNameAndIdString> lngs)
        {
            if (flag)
            {
                Languages = lngs;
                await OnInitializedAsync();
            }

        }
        protected override async Task OnInitializedAsync()
        {
            if (Languages.Count() == 0)
                Languages = await _languageService.GetAll();
            cultures = new List<CultureInfo>();
            foreach (var item in Languages)
            {
                cultures.Add(new CultureInfo(item.Id));
            }

        }
        protected void ChangeCulture(CultureInfo culture)
        {
            this.Culture = culture;
        }

        CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture != value)
                {
                    var js = (IJSInProcessRuntime)JSRuntime;
                    js.InvokeVoid("blazorCulture.set", value.Name);
                    NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
                }
            }
        }
    }
}
