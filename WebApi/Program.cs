using Application;
using Application.Services.JWT;
using CorePush.Apple;
using CorePush.Google;
using Domain.Entities;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Contexts;
using Serilog;
using System.Configuration;
using System.Globalization;
using System.Text;
using WebApi.Helpers;
using WebApi.Middleware;
using WebApi.PushNotification;
using WebApi.Services;
using WebApi.Services.SignalR;
using WebApi.Wrappers;

var builder = WebApplication.CreateBuilder(args);
// If using Kestrel:
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

// If using IIS:
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//Use Seri Log
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc
.ReadFrom.Configuration(ctx.Configuration)));

// Add services to the container.


builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
//Configuration from AppSettings
builder.Services.Configure<JWTDto>(builder.Configuration.GetSection("JWT"));
//User Manager Service
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

//Adding DB Context with MSSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
//Adding Athentication - JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
    o.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hub")))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        },

    };

});

builder.Services.AddHangfire(c => c.UseMemoryStorage());
builder.Services.AddHangfireServer();
builder.Services.AddControllers();
//
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddHttpClient<FcmSender>();
builder.Services.AddHttpClient<ApnSender>();
// Configure strongly typed settings objects
builder.Services.Configure<FcmNotificationSetting>(builder.Configuration.GetSection("FcmNotification"));
//

builder.Services.AddCors();
builder.Services.AddSignalR(c =>
{
    c.EnableDetailedErrors = true;
    c.ClientTimeoutInterval = TimeSpan.MaxValue;
    c.KeepAliveInterval = TimeSpan.MaxValue;
});
builder.Services.AddTransient<PresenceTracker>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.IncludeXmlComments(string.Format(@"{0}\WEU.xml", System.AppDomain.CurrentDomain.BaseDirectory));
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "WEU",
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddRazorPages();
builder.Services.AddResponseCaching();
builder.Services.AddDirectoryBrowser();
// Add API Versioning to the Project
builder.Services.AddApiVersioning(config =>
{
    // Specify the default API Version as 1.0
    config.DefaultApiVersion = new ApiVersion(1, 0);
    // If the client hasn't specified the API version in the request, use the default API version number 
    config.AssumeDefaultVersionWhenUnspecified = true;
    // Advertise the API versions supported for the particular endpoint
    config.ReportApiVersions = true;
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<OnlineUserMiddleWare>();
//app.UseMiddleware<AuthenticationMiddleware>();
app.UseSerilogRequestLogging();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        Log.Information("Application Starting.");
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await ApplicationDbContextSeed.SeedEssentialsAsync(userManager, roleManager, context);
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "The Application failed to start.");
    }
    finally
    {
        Log.CloseAndFlush();
    }
}
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEU");
});
//}

app.UseHangfireDashboard("/mydashboard");
app.UseHangfireServer();
app.UseHttpsRedirection();

///////////////
var localizationOptions = new RequestLocalizationOptions
{
    SupportedCultures = new List<CultureInfo>
    {
        new CultureInfo("de"),
        new CultureInfo("en"),
         new CultureInfo("fa")
    },
    SupportedUICultures = new List<CultureInfo>
    {
        new CultureInfo("de"),
        new CultureInfo("en"),
         new CultureInfo("fa")
    }
};
localizationOptions.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProviderExtension());
app.UseRequestLocalization(localizationOptions);


//////////////
app.UseFastReport();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles")),
    RequestPath = "/UploadedFiles"

});
app.UseDirectoryBrowser((new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles")),
    RequestPath = new PathString("/UploadedFiles")
}));
app.UseRouting();
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true)); // allow any origin
                                          //.WithOrigins("http://192.168.100.100", "http://192.168.100.100:8080", "https://localhost:7107", "https://localhost:7155", "https://localhost:7008","https://localhost:3000"));
app.UseAuthentication();
app.UseResponseCaching();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<PresenceHub>("/hub/presence");
    endpoints.MapHub<MessageHub>("/hub/chat");
    endpoints.MapControllerRoute("default", "api/v1/{controller=Home}/{action=Index}/{id?}");

});
app.MapRazorPages();
//app.MapHub<ChatHub>("/chat");
//app.UseMvc(routes => {
//    routes.MapRoute(name: "default", template: "api/v{version:apiVersion}/{culture:culture=fa-IR}/{controller=Home}/{action=Index}/{id?}");
//});

app.Run();
