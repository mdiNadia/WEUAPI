using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Application.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        /// <summary>
        /// https://github.com/dotnet/efcore/issues/
        /// علت استفاده کردن و نکردن از IApplicationDBcontext
        /// </summary>
        DbSet<Profile> Profiles { get; set; }
        DbSet<ProfileSetting> ProfileSettings { get; set; }
        DbSet<ProfileScore> ProfileScores { get; set; }
        DbSet<Advertising> Advertisings { get; set; }
        DbSet<AdCategory> AdCategories { get; set; }
        DbSet<Attachment> Attachments { get; set; }
        DbSet<FileType> FileTypes { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<ConfirmResult> ConfirmedResults { get; set; }
        DbSet<ConfirmedResultAttachment> ConfirmedResultAttachments { get; set; }
        DbSet<RejectedResultAttachment> RejectedResultAttachments { get; set; }
        DbSet<RejectResult> RejectedResults { get; set; }
        DbSet<SavedAd> SavedAds { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Like> Likes { get; set; }
        DbSet<LikeComment> LikeComments { get; set; }
        DbSet<Favorite> Favorites { get; set; }
        DbSet<View> Views { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Province> Provinces { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Neighborhood> Neighborhoods { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<Boost> Boosts { get; set; }
        DbSet<AppSetting> AppSettings { get; set; }
        DbSet<CurrencySetting> CurrencySettings { get; set; }
        DbSet<AdCategoryCost> AdCategoryCosts { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<UserLoginHistory> UsersLoginHistory { get; set; }
        //جدول های کیف پول و تراکنش
        DbSet<Wallet> Wallets { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<BankAccount> BankAccounts { get; set; }
        DbSet<TransactionType> TransactionTypes { get; set; }
        DbSet<TransactionStatus> TransactionStatuses { get; set; }
        //جدول های چت و سیگنال آر
        DbSet<Message> Messages { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Connection> Connections { get; set; }
        //جدول های فالور و بلاک و ریپورت
        DbSet<UserFollowing> UserFollowings { get; set; }
        DbSet<ProfileBlock> ProfileBlocks { get; set; }
        DbSet<ProfileReport> ProfileReports { get; set; }
        DbSet<ReportReason> ReportReasons { get; set; }
        DbSet<AdReport> AdReports { get; set; }
        //جدول های واسط
        DbSet<AdCategoryAdvertising> AdCategoryAdvertisings { get; set; }
        DbSet<AdvertisingAttachment> AdvertisingAttachments { get; set; }
        DbSet<AdCountry> AdCountries { get; set; }
        DbSet<AdProvince> AdProvinces { get; set; }
        DbSet<AdCity> AdCities { get; set; }
        DbSet<AdNeighborhood> AdNeighborhoods { get; set; }
        DbSet<ApplicationRole> ApplicationRoles { get; set; }
        DbSet<SendSmsCode> SendSmsCodes { get; set; }

        DbSet<TransferValueHistory> TransferValueHistories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderRow> OrderRows { get; set; }
        DbSet<Payment> Payments { get; set; }
        IDbContextTransaction DatabaseBeginTransaction();
        Task SaveChangesAsync();
        DbSet<TEntity> set<TEntity>() where TEntity : class;

    }
}
