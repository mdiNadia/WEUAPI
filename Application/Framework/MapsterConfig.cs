using Microsoft.Extensions.DependencyInjection;

namespace Application.Framework
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            //TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            //TypeAdapterConfig<Advertising, GetAdvertisingDto>
            //    .NewConfig()
            //    .Map(d => d.AdvertisingAttachments, s => s.AdvertisingAttachments);
        }
    }
}
