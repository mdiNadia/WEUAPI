using Microsoft.AspNetCore.Http;
using WEUPanel.Wrappers;

namespace WEUPanel.Pages.Profile
{
    public class ProfileModels
    {
        public class Profile
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Username { get; set; }
            public string Bio { get; set; }
            public string Link { get; set; }
            public int Score { get; set; }
            public int ProfileType { get; set; }
            public DateTime CreationDate { get; set; }
            public int FollowersCount { get; set; }
            public int FollowingCount { get; set; }
            public int BlockedCount { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public GetNameAndIdString User { get; set; }
            public int? AvatarId { get; set; }
            public string? AvatarName { get; set; }
        }
        public class CreateProfile { }
        public class EditProfile
        {
            public int Id { get; set; }
            public IFormFile AvatarFile { get; set; }
            public int AvatarId { get; set; }
            public string AvatarName { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
            public string Link { get; set; }
            public int ProfileType { get; set; }
        }
    }
}
