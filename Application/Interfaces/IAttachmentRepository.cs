using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IAttachmentRepository : IGenericRepository<Attachment>
    {
        Task<List<int>> ListOfAdNewImageIds(List<IFormFile> Images, string folderName);
        Task<int> NewFileId(IFormFile file, string folderName);
        Task<List<int>> ListOfAdNewVideoIds(List<IFormFile> Videos, string folderName);
    }


}
