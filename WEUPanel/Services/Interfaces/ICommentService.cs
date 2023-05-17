using WEUPanel.Pages.Comment;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<CommentModels.Comment>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<CommentModels.Comment>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(CommentModels.CreateComment command);
        Task<HttpResponseMessage> UpdateEntity(int id, CommentModels.EditComment
            command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);
    }
}
