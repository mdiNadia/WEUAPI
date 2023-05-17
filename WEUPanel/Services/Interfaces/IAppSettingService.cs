using WEUPanel.Pages.AppSetting;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IAppSettingService
    {
        Task<Response<AppSettingModels.AppSetting>> GetAppSetting();
        Task<HttpResponseMessage> UpdateEntity(int id, AppSettingModels.EditAppSetting command);
    }
}
