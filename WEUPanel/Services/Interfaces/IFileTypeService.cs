
using WEUPanel.Pages.FileType;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IFileTypeService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<FileTypeModels.FileType>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<FileTypeModels.FileType>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(FileTypeModels.CreateFileType command);
        Task<HttpResponseMessage> UpdateEntity(int id, FileTypeModels.EditFileType command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);
    }
}
