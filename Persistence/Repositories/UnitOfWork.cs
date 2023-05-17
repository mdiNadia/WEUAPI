using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        public IProfileRepository Profiles { get; private set; }
        public IUserLoginHistoryRepository UsersLoginHistory { get; private set; }
        public IProfileSettingRepository ProfileSettings { get; private set; }
        public IAdCategoryRepository AdCategories { get; private set; }

        public IProfileScoreRepository ProfileScores { get; private set; }
        public IAdvertisingRepository Advertisings { get; private set; }
        public IAttachmentRepository Attachments { get; private set; }
        public IAdvertisingAttachmentRepository AdvertisingAttachments { get; private set; }
        public IAdCategoryAdvertisingRepository AdCategoryAdvertisings { get; private set; }
        public ISavedAdRepository SavedAds { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IWalletRepository Wallets { get; private set; }
        public ITransactionRepository Transactions { get; private set; }
        public IBankAccountRepository BankAccounts { get; private set; }
        public IMessageRepository Messages { get; private set; }
        public ITransactionTypeRepository TransactionTypes { get; private set; }
        public ITransactionStatusRepository TransactionStatuses { get; private set; }
        public IUserFollowingsRepository UserFollowings { get; private set; }
        public IProfileBlocksRepository ProfileBlocks { get; private set; }
        public IReportReasonRepository ReportReasons { get; private set; }
        public IProfileReportRepository ProfileReports { get; private set; }
        public IAdReportRepository AdReports { get; private set; }
        public ILanguageRepository Languages { get; private set; }
        public ILikeRepository Likes { get; private set; }
        public ILikeCommentRepository LikeComments { get; private set; }
        public IFavoriteRepository Favorites { get; private set; }
        public IViewRepository Views { get; private set; }
        public ICountryRepository Countries { get; private set; }
        public ICurrencyRepository Currencies { get; private set; }
        public ICurrencySettingRepository CurrencySettings { get; private set; }
        public IBoostRepository Boosts { get; private set; }
        public IAppSettingRepository AppSettings { get; private set; }
        public IProvinceRepository Provinces { get; private set; }

        public IUserRepository Users { get; private set; }
        public IRejectResultRepository RejectResults { get; private set; }
        public ICityRepository Cities { get; private set; }
        public INeighborhoodRepository Neighborhoods { get; private set; }
        public IAdCategoryCostRepository AdCategoryCosts { get; private set; }

        public IAdCountryRepository AdCountries { get; private set; }

        public IAdProvinceRepository AdProvinces { get; private set; }

        public IAdCityRepository AdCities { get; private set; }
        public IConfirmedResultRepository ConfirmedResults { get; private set; }
        public IAdNeighborhoodRepository AdNeighborhoods { get; private set; }
        public IConfirmedResultAttachmentRepository ConfirmedResultAttachments { get; private set; }
        public IFileTypeRepository FileTypes { get; private set; }
        public IRejectedResultAttachmentRepository RejectedResultAttachments { get; private set; }
        public IApplicationRoleRepository ApplicationRoles { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderRowRepository OrderRows { get; private set; }
        public ITransferValueHistoryRepository TransferValueHistories { get; private set; }
        public ISendSmsCodeRepository SendSmsCodes { get; private set; }
        public IPaymentRepository Payments { get; private set; }

        public UnitOfWork(IApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IProfileScoreRepository ProfileScore,
            IProfileRepository Profile,
            IProfileSettingRepository ProfileSetting,
            IAdCategoryRepository AdCategorie,
            IAdvertisingRepository Advertising,
            IAttachmentRepository Attachment,
            IAdCategoryAdvertisingRepository AdCategoryAdvertising,
            IAdvertisingAttachmentRepository AdvertisingAttachment,
            ICommentRepository Comment,
            ISavedAdRepository SavedAd,
            IMessageRepository Message,
            IWalletRepository Wallet,
            ITransactionRepository Transaction,
            IBankAccountRepository BankAccount,
            ITransactionTypeRepository TransactionType,
            IUserFollowingsRepository UserFollowing,
            IProfileBlocksRepository ProfileBlock,
            IReportReasonRepository ReportReason,
            IProfileReportRepository ProfileReport,
            IAdReportRepository AdReport,
            ILanguageRepository Language,
            ILikeRepository Like,
            IFavoriteRepository favorite,
            IViewRepository View,
            ICountryRepository Country,
            ICurrencyRepository Currency,
            ICurrencySettingRepository CurrencySetting,
             IUserRepository User,
             IRejectResultRepository RejectResult,
             IBoostRepository Boost,
             IAppSettingRepository BoostSetting,
             IProvinceRepository Province,
             ICityRepository City,
             INeighborhoodRepository Neighborhood,
             IAdCategoryCostRepository AdCategoryCost,
             IAdCountryRepository AdCountry,
             IAdProvinceRepository AdProvince,
             IAdCityRepository AdCity,
             IAdNeighborhoodRepository AdNeighborhood,
            IConfirmedResultRepository ConfirmedResult,
            IConfirmedResultAttachmentRepository ConfirmedResultAttachment,
             IRejectedResultAttachmentRepository RejectedResultAttachment,
              IFileTypeRepository FileType,
              ILikeCommentRepository LikeComment,
              IUserLoginHistoryRepository UserLoginHistory,
              IApplicationRoleRepository ApplicationRole,
              ISendSmsCodeRepository sendSmsCode,
              ITransactionStatusRepository transactionStatus,
              ITransferValueHistoryRepository transferValueHistory,
              IOrderRepository orderRepository,
              IOrderRowRepository orderRowRepository,
              IPaymentRepository paymentRepository
            )
        {

            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            ProfileScores = new ProfileScoreRepository(this._context, this._httpContextAccessor);
            Profiles = new ProfileRepository(this._context, this._httpContextAccessor);
            ProfileSettings = new ProfileSettingRepository(this._context, this._httpContextAccessor);
            AdCategories = new AdCategoryRepository(this._context, this._httpContextAccessor);
            Advertisings = new AdvertisingRepository(this._context, this._httpContextAccessor);
            Attachments = new AttachmentRepository(this._context, this._httpContextAccessor);
            AdCategoryAdvertisings = new AdCategoryAdvertisingRepository(this._context, this._httpContextAccessor);
            AdvertisingAttachments = new AdvertisingAttachmentRepository(this._context, this._httpContextAccessor);
            Comments = new CommentRepository(this._context, this._httpContextAccessor);
            Messages = new MessageRepository(this._context, this._httpContextAccessor);
            Wallets = new WalletRepository(this._context, this._httpContextAccessor);
            BankAccounts = new BankAccountRepository(this._context, this._httpContextAccessor);
            Transactions = new TransactionRepository(this._context, this._httpContextAccessor);
            TransactionTypes = new TransactionTypeRepository(this._context, this._httpContextAccessor);
            UserFollowings = new UserFollowingsRepository(this._context, this._httpContextAccessor);
            ProfileBlocks = new ProfileBlocksRepository(this._context, this._httpContextAccessor);
            ReportReasons = new ReportReasonRepository(this._context, this._httpContextAccessor);
            ProfileReports = new ProfileReportRepository(this._context, this._httpContextAccessor);
            AdReports = new AdReportRepository(this._context, this._httpContextAccessor);
            Languages = new LanguageRepository(this._context, this._httpContextAccessor);
            Likes = new LikeRepository(this._context, this._httpContextAccessor);
            Favorites = new FavoriteRepository(this._context, this._httpContextAccessor);
            Views = new ViewRepository(this._context, this._httpContextAccessor);
            SavedAds = new SavedAdRepository(this._context, this._httpContextAccessor);
            Countries = new CountryRepository(this._context, this._httpContextAccessor);
            Currencies = new CurrencyRepository(this._context, this._httpContextAccessor);
            CurrencySettings = new CurrencySettingRepository(this._context, this._httpContextAccessor);
            Users = new UserRepository(this._context, this._httpContextAccessor);
            Boosts = new BoostRepository(this._context, this._httpContextAccessor);
            AppSettings = new AppSettingRepository(this._context, this._httpContextAccessor);
            Provinces = new ProvinceRepository(this._context, this._httpContextAccessor);
            RejectResults = new RejectResultRepository(this._context, this._httpContextAccessor);
            Cities = new CityRepository(this._context, this._httpContextAccessor);
            Neighborhoods = new NeighborhoodRepository(this._context, this._httpContextAccessor);
            AdCategoryCosts = new AdCategoryCostRepository(this._context, this._httpContextAccessor);
            AdCountries = new AdCountryRepository(this._context, this._httpContextAccessor);
            AdProvinces = new AdProvinceRepository(this._context, this._httpContextAccessor);
            AdCities = new AdCityRepository(this._context, this._httpContextAccessor);
            AdNeighborhoods = new AdNeighborhoodRepository(this._context, this._httpContextAccessor);
            ConfirmedResults = new ConfirmedResultRepository(this._context, this._httpContextAccessor);
            ConfirmedResultAttachments = new ConfirmedResultAttachmentRepository(this._context, this._httpContextAccessor);
            RejectedResultAttachments = new RejectedResultAttachmentRepository(this._context, this._httpContextAccessor);
            FileTypes = new FileTypeRepository(this._context, this._httpContextAccessor);
            LikeComments = new LikeCommentRepository(this._context, this._httpContextAccessor);
            UsersLoginHistory = new UserLoginHistoryRepository(this._context, this._httpContextAccessor);
            ApplicationRoles = new ApplicationRoleRepository(this._context, this._httpContextAccessor);
            SendSmsCodes = new SendSmsCodeRepository(this._context, this._httpContextAccessor);
            TransactionStatuses = new TransactionStatusRepository(this._context, this._httpContextAccessor);
            TransferValueHistories = new TransferValueHistoryRepository(this._context, this._httpContextAccessor);
            Orders = new OrderRepository(this._context, this._httpContextAccessor);
            OrderRows = new OrderRowRepository(this._context, this._httpContextAccessor);
            Payments = new PaymentRepository(this._context, this._httpContextAccessor);
        }
        public async Task CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.DatabaseBeginTransaction();
        }


    }
}

