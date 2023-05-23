
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IProfileRepository Profiles { get; }
        IProfileScoreRepository ProfileScores { get; }
        IProfileSettingRepository ProfileSettings { get; }
        //advertisement
        IAdCategoryRepository AdCategories { get; }
        IAdvertisingRepository Advertisings { get; }
        IAttachmentRepository Attachments { get; }
        IAdCategoryAdvertisingRepository AdCategoryAdvertisings { get; }

        IAdvertisingAttachmentRepository AdvertisingAttachments { get; }

        ISavedAdRepository SavedAds { get; }
        IRejectResultRepository RejectResults { get; }
        //comment, chat
        ICommentRepository Comments { get; }
        IMessageRepository Messages { get; }

        //Wallet
        IWalletRepository Wallets { get; }
        IBankAccountRepository BankAccounts { get; }
        ITransactionRepository Transactions { get; }
        ITransactionTypeRepository TransactionTypes { get; }
        ITransactionStatusRepository TransactionStatuses { get; }
        //follow,block,report
        IUserFollowingsRepository UserFollowings { get; }
        IProfileBlocksRepository ProfileBlocks { get; }
        IReportReasonRepository ReportReasons { get; }
        IProfileReportRepository ProfileReports { get; }
        IAdReportRepository AdReports { get; }
        ILanguageRepository Languages { get; }
        ILikeRepository Likes { get; }
        ILikeCommentRepository LikeComments { get; }
        IFavoriteRepository Favorites { get; }
        IViewRepository Views { get; }
        //Currency and exchange
        ICountryRepository Countries { get; }
        ICurrencyRepository Currencies { get; }
        ICurrencySettingRepository CurrencySettings { get; }
        IBoostRepository Boosts { get; }
        IAppSettingRepository AppSettings { get; }
        IProvinceRepository Provinces { get; }
        ICityRepository Cities { get; }
        INeighborhoodRepository Neighborhoods { get; }
        IAdCategoryCostRepository AdCategoryCosts { get; }
        IUserRepository Users { get; }
        IAdCountryRepository AdCountries { get; }
        IAdProvinceRepository AdProvinces { get; }
        IAdCityRepository AdCities { get; }
        IAdNeighborhoodRepository AdNeighborhoods { get; }
        IConfirmedResultRepository ConfirmedResults { get; }
        IConfirmedResultAttachmentRepository ConfirmedResultAttachments { get; }
        IFileTypeRepository FileTypes { get; }
        IRejectedResultAttachmentRepository RejectedResultAttachments { get; }
        IUserLoginHistoryRepository UsersLoginHistory { get; }
        IApplicationRoleRepository ApplicationRoles { get; }
        ISendSmsCodeRepository SendSmsCodes { get; }
        ITransferValueHistoryRepository TransferValueHistories { get; }
        IOrderRepository Orders { get; }
        IOrderRowRepository OrderRows { get; }
        IPaymentRepository Payments { get; }
        INotificationRepository Notifications { get; }
        Task CompleteAsync();
        IDbContextTransaction BeginTransaction();
    }
}
