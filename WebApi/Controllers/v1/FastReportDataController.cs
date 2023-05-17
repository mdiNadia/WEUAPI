using Application.Features.ReportData.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FastReportDataController : BaseApiController
    {
        private readonly IUriService _uriService;

        public FastReportDataController(IUriService uriService)
        {
            this._uriService = uriService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserLoginHistory([FromQuery] ReportQuery query)
        {
            try
            {
                byte[] result = await Mediator.Send(new GetUsersLoginHistory { format = query.Format });
                return File(result, "application/" + query.Format, "result." + query.Format);
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}


