
using Application.Framework;

using Application.Services.Email;
using Application.Services.FileStorage;
using Application.Services.jobs;
using Application.Services.JWT;
using Application.Services.Sms;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing.Text;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());
            //
            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //services.AddScoped<IUrlHelper>(x =>
            //{
            //    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
            //    var factory = x.GetRequiredService<IUrlHelperFactory>();
            //    return factory.GetUrlHelper(actionContext);
            //});
            //
            services.RegisterMapsterConfiguration();//Mapster is used instead of AutoMapping
            services.AddScoped<IJWTGenerator, JWTGenerator>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<ISmsSender, SmsSender>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IFileUploader, FileUploader>();
            services.AddScoped<IUserAccessor, UserAccessor>();


        }
    
    }
}
