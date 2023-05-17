using Microsoft.AspNetCore.Http;


namespace Application.Services.FileStorage
{
    public interface IFileUploader
    {
        Task<string> UploadFile(IFormFile file, string folderName);
        Task<List<string>> UploadFiles(List<IFormFile> files);
        Task<bool> DeleteFile(string ImgName, string folderName);
        Task<bool> DeleteFiles(List<string> ImgsName, string folderName);
    }
}
