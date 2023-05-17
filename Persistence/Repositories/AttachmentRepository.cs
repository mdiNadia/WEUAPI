using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        private readonly IMediator _mediator;
        private readonly IFileUploader _fileUploader;
        public AttachmentRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public AttachmentRepository(IMediator mediator, IFileUploader fileUploader, IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            this._mediator = mediator;
            this._fileUploader = fileUploader;
        }
        public async Task<List<int>> ListOfAdNewImageIds(List<IFormFile> Images, string folderName)
        {

            List<int> listOfIds = new List<int>();
            foreach (var item in Images)
            {
                CreateAttachment createAttachment = new CreateAttachment();
                createAttachment.Name = "";
                createAttachment.Description = "";
                //createAttachment.FileType = 0;
                createAttachment.File = item;
                createAttachment.FolderName = folderName;
                var fileId = await _mediator.Send(createAttachment);
                listOfIds.Add(fileId);
            }
            while (listOfIds.Count() < 8)
            {
                CreateAttachment createAttachment = new CreateAttachment();
                createAttachment.Name = "";
                createAttachment.Description = "";
                //createAttachment.FileType = 0;
                createAttachment.File = null;
                createAttachment.FolderName = folderName;
                var fileId = await _mediator.Send(createAttachment);
                listOfIds.Add(fileId);
            }
            return listOfIds;

        }
        public async Task<int> NewFileId(IFormFile file, string folderName)
        {

            int id = new();
            CreateAttachment createAttachment = new CreateAttachment();
            createAttachment.Name = "";
            createAttachment.Description = "";
            //createAttachment.FileType = 0;
            createAttachment.File = file;
            createAttachment.FolderName = folderName;
            var fileId = await _mediator.Send(createAttachment);
            id = fileId;
            return id;

        }
        public async Task<List<int>> ListOfAdNewVideoIds(List<IFormFile> Videos, string folderName)
        {

            List<int> listOfIds = new List<int>();
            foreach (var item in Videos)
            {
                CreateAttachment createAttachment = new CreateAttachment();
                createAttachment.Name = "";
                createAttachment.Description = "";
                //createAttachment.FileType = 0;
                createAttachment.File = item;
                createAttachment.FolderName = folderName;
                var fileId = await _mediator.Send(createAttachment);
                listOfIds.Add(fileId);
            }
            while (listOfIds.Count() < 8)
            {
                CreateAttachment createAttachment = new CreateAttachment();
                createAttachment.Name = "";
                createAttachment.Description = "";
                //createAttachment.FileType = 0;
                createAttachment.File = null;
                createAttachment.FolderName = folderName;
                var fileId = await _mediator.Send(createAttachment);
                listOfIds.Add(fileId);
            }
            return listOfIds;

        }
    }
}
