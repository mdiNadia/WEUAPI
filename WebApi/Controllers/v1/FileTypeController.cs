using Application.Features.FileType.Commands;
using Application.Features.FileType.Queries;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FileTypeController : BaseApiController
    {
        private readonly IUriService _uriService;

        public FileTypeController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New FileType.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateFileType command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all FileTypes with paging filter.
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetAll")]
        public async Task<object> GetAll(DataSourceLoadOptions loadOptions)
        {
            var result = await Mediator.Send(new GetAllFileTypes());
            return DataSourceLoader.Load(result, loadOptions);
        }
        /// <summary>
        /// Gets FileType Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var FileType = await Mediator.Send(new GetFileTypeById { Id = id });
            return Ok(new Response<GetFileTypeDto>(FileType));
        }
        /// <summary>
        /// Deletes FileType Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteFileTypeById { Id = id }));
        }
        /// <summary>
        /// Updates the FileType Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateFileType command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
