using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WEUPanel;
using WEUPanel.Extensions;
using WEUPanel.Helpers;
using WEUPanel.Services;
using WEUPanel.Services.Account;
using WEUPanel.Services.Interfaces;
using WEUPanel.Shared;
using WEUPanel.Shared.Common;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddLocalization();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddAntDesign();
builder.Services.AddScoped<BaseRequestParameter>();
builder.Services.TryAddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
#region 
builder.Services.AddScoped<State>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IAppSettingService, AppSettingService>();
builder.Services.AddScoped<IProfileScoreService, ProfileScoreService>();
builder.Services.AddScoped<IAdvertiseCategory, AdvertiseCategory>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ICurrencySettingService, CurrencySettingService>();
builder.Services.AddScoped<IAdveriseService, AdvertiseService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IReportReasonService, ReportReasonService>();
builder.Services.AddScoped<IReportedService, ReportedService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<INeighborhoodService, NeighborhoodService>();
builder.Services.AddScoped<IAdCategoryCostService, AdCategoryCostService>();
builder.Services.AddScoped<IConfirmedResultservice, ConfirmedResultservice>();
builder.Services.AddScoped<IFileTypeService, FileTypeService>();
builder.Services.AddScoped<IRejectedResultService, RejectedResultService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<CultureSelector>();
#endregion Services

var host = builder.Build();
await host.SetDefaultCulture();
await host.RunAsync();

