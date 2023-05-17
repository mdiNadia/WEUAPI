using Microsoft.AspNetCore.Localization;

namespace WebApi.Helpers
{
    public class RouteDataRequestCultureProviderExtension : RequestCultureProvider
    {
        public int IndexOfCulture;
        public int IndexofUICulture;
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            string culture = null;
            string uiCulture = null;
            var userLangs = httpContext.Request.Headers["Accept-Language"].ToString();
            var firstLang = userLangs.Split(',').FirstOrDefault();
            culture = uiCulture = string.IsNullOrEmpty(firstLang) ? "fa" : firstLang.Split('-').FirstOrDefault();
            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);
            return Task.FromResult(providerResultCulture);
        }
    }
}
