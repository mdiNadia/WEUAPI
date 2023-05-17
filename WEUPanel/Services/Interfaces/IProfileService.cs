using WEUPanel.Pages.Profile;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IProfileService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<ProfileModels.Profile>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<ProfileModels.Profile>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(ProfileModels.CreateProfile command);
        Task<HttpResponseMessage> UpdateEntity(int id, ProfileModels.EditProfile command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);
    }
}
