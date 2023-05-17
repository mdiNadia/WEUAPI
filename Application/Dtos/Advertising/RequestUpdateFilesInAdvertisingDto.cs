using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Advertising
{
    public class RequestUpdateFilesInAdvertisingDto
    {
        public int AttachmentId { get; set; }
        public bool IsChanged { get; set; }
        public Domain.Enums.FileType FileType { get; set; }
        public IFormFile UpdatedFile { get; set; }
    }
}
