using Application.Builders;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileSetting> ProfileSettings { get; set; }
        public DbSet<ProfileScore> ProfileScores { get; set; }
        public DbSet<AdCategory> AdCategories { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AdCategoryAdvertising> AdCategoryAdvertisings { get; set; }
        public DbSet<AdvertisingAttachment> AdvertisingAttachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikeComment> LikeComments { get; set; }
        public DbSet<SavedAd> SavedAds { get; set; }
        public DbSet<Domain.Entities.Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Domain.Entities.Connection> Connections { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }
        public DbSet<ProfileBlock> ProfileBlocks { get; set; }
        public DbSet<ReportReason> ReportReasons { get; set; }
        public DbSet<ProfileReport> ProfileReports { get; set; }
        public DbSet<AdReport> AdReports { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencySetting> CurrencySettings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<RejectResult> RejectedResults { get; set; }
        public DbSet<Boost> Boosts { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TransferValueHistory> TransferValueHistories { get; set; }
        public DbSet<AdCategoryCost> AdCategoryCosts { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<AdCountry> AdCountries { get; set; }
        public DbSet<AdProvince> AdProvinces { get; set; }
        public DbSet<AdCity> AdCities { get; set; }
        public DbSet<AdNeighborhood> AdNeighborhoods { get; set; }
        public DbSet<ConfirmResult> ConfirmedResults { get; set; }
        public DbSet<ConfirmedResultAttachment> ConfirmedResultAttachments { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<UserLoginHistory> UsersLoginHistory { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<RejectedResultAttachment> RejectedResultAttachments { get; set; }
        public DbSet<SendSmsCode> SendSmsCodes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public IDbContextTransaction DatabaseBeginTransaction() { return Database.BeginTransaction(); }
        public DbSet<TEntity> set<TEntity>() where TEntity : class { return Set<TEntity>(); }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<Profile>(new ProfileBuilder());
            builder.ApplyConfiguration<ApplicationUser>(new UserBuilder());
            builder.ApplyConfiguration<AdCategory>(new AdCategoryBuilder());
            builder.ApplyConfiguration<Advertising>(new AdvertisingBuilder());
            builder.ApplyConfiguration<Attachment>(new AttachmentBuilder());
            builder.ApplyConfiguration<Message>(new MessageBuilder());
            builder.ApplyConfiguration<Wallet>(new WalletBuilder());
            builder.ApplyConfiguration<Transaction>(new TransactionBuilder());
            builder.ApplyConfiguration<UserFollowing>(new UserFollowingBuilder());
            builder.ApplyConfiguration<ProfileBlock>(new ProfileBlockBuilder());
            builder.ApplyConfiguration<ProfileReport>(new ProfileReportBuilder());
            builder.ApplyConfiguration<AdReport>(new AdReportBuilder());
            builder.ApplyConfiguration<ReportReason>(new ReportReasonBuilder());
            builder.ApplyConfiguration<Like>(new LikeBuilder());
            builder.ApplyConfiguration<View>(new ViewBuilder());
            builder.ApplyConfiguration<SavedAd>(new SavedAdBuilder());
            builder.ApplyConfiguration<Country>(new CountryBuilder());
            builder.ApplyConfiguration<Province>(new ProvinceBuilder());
            builder.ApplyConfiguration<City>(new CityBuilder());
            builder.ApplyConfiguration<Boost>(new BoostBuilder());
            builder.ApplyConfiguration<Currency>(new CurrencyBuilder());
            builder.ApplyConfiguration<AdCategoryCost>(new AdCategoryCostBuilder());
            builder.ApplyConfiguration<CurrencySetting>(new CurrencySettingBuilder());
            builder.ApplyConfiguration<FileType>(new FileTypeBuilder());
            builder.ApplyConfiguration<AdvertisingAttachment>(new AdvertisingAttachmentBuilder());
            builder.ApplyConfiguration<ConfirmedResultAttachment>(new ConfirmedResultAttachmentBuilder());
            builder.ApplyConfiguration<Favorite>(new FavoriteBuilder());
            builder.ApplyConfiguration<LikeComment>(new LinkeCommentBuilder());
            builder.ApplyConfiguration<RejectedResultAttachment>(new RejectedResultAttachmentBuilder());
            builder.ApplyConfiguration<TransferValueHistory>(new TransferValueHistoryBuilder());
            builder.ApplyConfiguration<Notification>(new NotificationBuilder());
            base.OnModelCreating(builder);
        }
        Task IApplicationDbContext.SaveChangesAsync()
        {
            return SaveChangesAsync();
        }
    }
}
