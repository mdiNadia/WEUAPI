using WEUPanel.Pages.ProfileScore;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IProfileScoreService
    {
        Task<IEnumerable<ProfileScoreModels.ProfileScore>> GetAll();
        Task<PagedResponse<IEnumerable<ProfileScoreModels.ProfileScore>>> GetAllByPaging(int pageIndex, int pageSize);

        Task<Response<ProfileScoreModels.ProfileScore>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(ProfileScoreModels.CreateProfileScore command);
        Task<HttpResponseMessage> UpdateEntity(int id, ProfileScoreModels.EditProfileScore command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);
    }
}
