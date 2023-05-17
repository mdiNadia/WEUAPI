
using WEUPanel.Pages.UserRole;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<List<GetNameAndIdString>> GetAll();

        Task<PagedResponse<IEnumerable<UserRoleModels.Role>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<UserRoleModels.Role>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(UserRoleModels.CreateRole command);
        Task<HttpResponseMessage> UpdateEntity(int id, UserRoleModels.EditRole command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);

    }
}
