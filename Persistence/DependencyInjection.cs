using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;


namespace Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISendSmsCodeRepository, SendSmsCodeRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProfileScoreRepository, ProfileScoreRepository>();
            services.AddScoped<IProfileSettingRepository, ProfileSettingRepository>();

            services.AddScoped<IAdCategoryRepository, AdCategoryRepository>();

            services.AddScoped<IAdCategoryRepository, AdCategoryRepository>();

            services.AddScoped<IAdvertisingRepository, AdvertisingRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();

            services.AddScoped<IAdCategoryAdvertisingRepository, AdCategoryAdvertisingRepository>();

            services.AddScoped<IAdvertisingAttachmentRepository, AdvertisingAttachmentRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeCommentRepository, LikeCommentRepository>();
            services.AddScoped<ISavedAdRepository, SavedAdRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            //Wallet Services
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();



            services.AddScoped<IUserFollowingsRepository, UserFollowingsRepository>();
            services.AddScoped<IProfileBlocksRepository, ProfileBlocksRepository>();
            services.AddScoped<IReportReasonRepository, ReportReasonRepository>();
            services.AddScoped<IProfileReportRepository, ProfileReportRepository>();

            services.AddScoped<IAdReportRepository, AdReportRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IViewRepository, ViewRepository>();
            services.AddScoped<ITransactionStatusRepository, TransactionStatusRepository>();
            //
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            services.AddScoped<IBoostRepository, BoostRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<INeighborhoodRepository, NeighborhoodRepository>();
            //
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICurrencySettingRepository, CurrencySettingRepository>();
            services.AddScoped<IRejectResultRepository, RejectResultRepository>();
            services.AddScoped<IAdCategoryCostRepository, AdCategoryCostRepository>();
            services.AddScoped<IAdCountryRepository, AdCountryRepository>();

            services.AddScoped<IAdProvinceRepository, AdProvinceRepository>();

            services.AddScoped<IAdCityRepository, AdCityRepository>();

            services.AddScoped<IAdNeighborhoodRepository, AdNeighborhoodRepository>();
            services.AddScoped<IConfirmedResultRepository, ConfirmedResultRepository>();
            services.AddScoped<IConfirmedResultAttachmentRepository, ConfirmedResultAttachmentRepository>();
            services.AddScoped<IRejectedResultAttachmentRepository, RejectedResultAttachmentRepository>();
            services.AddScoped<IUserLoginHistoryRepository, UserLoginHistoryRepository>();
            services.AddScoped<IFileTypeRepository, FileTypeRepository>();
            services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();
            services.AddScoped<ITransferValueHistoryRepository, TransferValueHistoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderRowRepository, OrderRowRepository>();
            services.AddScoped<IFileTypeRepository, FileTypeRepository>();
            services.AddScoped<IPaymentRepository,PaymentRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
