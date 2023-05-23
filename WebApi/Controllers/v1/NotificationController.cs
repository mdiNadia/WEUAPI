
using Application.Features.City.Queries;
using Application.Features.Notification.Commands;
using Application.Features.Notification.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.PushNotification;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;
        private readonly IUriService _uriService;

        public NotificationController(INotificationService notificationService, IUriService uriService)
        {
            _notificationService = notificationService;
            this._uriService = uriService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {

            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

        /// <summary>
        /// Creates a New Notification.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateNotification command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Gets all Notifications with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllNotifications(filter));
            var totalRecords = await Mediator.Send(new GetAllCountNotifications());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetNotificationDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        /// <summary>
        /// Gets Notification Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var City = await Mediator.Send(new GetNotificationById { Id = id });
            return Ok(new Response<GetNotificationDto>(City));
        }
    }
}
