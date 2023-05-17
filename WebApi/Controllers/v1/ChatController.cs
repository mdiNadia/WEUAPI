using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Services;
using WebApi.Services.SignalR;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]

    public class ChatsController : BaseApiController
    {
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly IUriService _uriService;

        public ChatsController(IHubContext<MessageHub> hubContext, IUriService uriService)
        {
            _hubContext = hubContext;
            _uriService = uriService;
        }

        /// <summary>
        /// Send a Direct Message.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpPost("PrivateMessage")]
        //public async Task<IActionResult> SendMessage([FromBody]CreatePrivateChat command)
        //{
        //    try
        //    {

        //        await _hubContext.Clients.Client(command.RecieverId.ToString()).SendAsync("ReceiveMessage", command.SenderId.ToString(), command.Message);

        //        return Ok();
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //}
    }
}
