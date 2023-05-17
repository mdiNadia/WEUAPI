using WEUPanel.Pages.Message;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IChatService
    {
        Task<PagedResponse<IEnumerable<MessageModels.Message>>> GetAllByPaging(string username, int pageIndex, int pageSize);

    }
}
