using WEUPanel.Pages.User;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<GetNameAndIdString>> GetAll();

        Task<PagedResponse<IEnumerable<UserModels.User>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<UserModels.User>> GetById(string id);
        Task<int> GetAllCount();
        Task<HttpResponseMessage> AddEntity(UserModels.CreateUser command);
        Task<HttpResponseMessage> UpdateEntity(string id, UserModels.EditUser command);
        Task<HttpResponseMessage> RemoveEntity(string id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(string id, MultipartFormDataContent command);
    }
}
