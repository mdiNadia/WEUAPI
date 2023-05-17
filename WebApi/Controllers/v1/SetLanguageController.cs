

using Application.Features.SetLanguage.Commands;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SetLanguageController : BaseApiController
    {
        private readonly IUriService _uriService;

        public SetLanguageController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture">زبانِ جدید است</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetLanguage(string culture = "fa"
            //string returnUrl
            )
        {
            //Response.Cookies.Append(
            //    CookieRequestCultureProvider.DefaultCookieName,
            //    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            //    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            //);
            return Ok(await Mediator.Send(new UpdateProfileLanguageByUsername { Language = culture }));
        }


    }
}
