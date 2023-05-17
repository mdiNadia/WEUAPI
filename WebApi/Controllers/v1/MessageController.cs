using Application.Helpers;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class MessageController : BaseApiController
    {
        private readonly IUriService _uriService;
        private readonly IUserAccessor _userAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public MessageController(IUriService uriService, IUserAccessor userAccessor, IUnitOfWork unitOfWork)
        {
            this._uriService = uriService;
            this._userAccessor = userAccessor;
            this._unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser([FromQuery]
            MessageParams messageParams, [FromQuery] PaginationFilter filter)
        {
            messageParams.Username = _userAccessor.GetCurrentUserNameAsync();
            //var messages = await _unitOfWork.Messages.GetMessagesForUser(messageParams);
            //return Ok(messages);

            var route = Request.Path.Value;
            var pagedData = await _unitOfWork.Messages.GetMessagesForUser(messageParams);
            var totalRecords = pagedData.Select(c => c.Id).Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Message>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = _userAccessor.GetCurrentUserNameAsync();

            var message = await _unitOfWork.Messages.GetMessage(id);

            if (message.Sender.UserName != username && message.Recipient.UserName != username)
                return Unauthorized();

            if (message.Sender.UserName == username) message.SenderDeleted = true;

            if (message.Recipient.UserName == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
                _unitOfWork.Messages.DeleteMessage(message);

            await _unitOfWork.CompleteAsync();
            return Ok();

            return BadRequest("Problem deleting the message");
        }
    }
}
