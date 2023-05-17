using Domain.Common;
using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Profile : BaseEntity
    {

        public string Name { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Link { get; set; }
        public int Score { get; set; }
        public ProfileType ProfileTypeEnum { get; set; }


        [JsonPropertyName("following")]
        public bool IsFollowed { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }

        public DateTime Birthday { get; set; } = new DateTime();
        public Gender Gender { get; set; } = 0;

        //تعداد افرادی که این شخص آنها را بلاک کرده است
        public int BlockedCount { get; set; }
        //رابطه ها
        public string UserId { get; set; } = "";
        public ApplicationUser User { get; set; } = new ApplicationUser();

        public int? AvatarId { get; set; } = null;
        public Attachment? Avatar { get; set; } = null;
        public ICollection<Advertising> Advertisings { get; set; }
        public ICollection<SavedAd> ProfileSavedAdvertisings { get; set; }


        public ICollection<UserFollowing> Followings { get; set; }
        public ICollection<UserFollowing> Followers { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public ICollection<ProfileBlock> BlockedUsers { get; set; }
        public ICollection<ProfileBlock> BlockerUsers { get; set; }


        public ICollection<ProfileReport> Reporters { get; set; }
        public ICollection<ProfileReport> Reporteds { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        //برای اگهی
        public ICollection<AdReport> AdReporters { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<View> Views { get; set; }

        public ICollection<TransferValueHistory> TransfererCoins { get; set; }
        public ICollection<TransferValueHistory> RecieverCoins { get; set; }
    }
}
