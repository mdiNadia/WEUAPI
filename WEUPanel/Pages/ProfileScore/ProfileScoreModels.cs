using Microsoft.AspNetCore.Http;

namespace WEUPanel.Pages.ProfileScore
{
    public class ProfileScoreModels
    {
        public class ProfileScore
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Score { get; set; }
            public int ProfileType { get; set; }
            public int? IconId { get; set; }
            public string? IconName { get; set; }
            public DateTime CreationDate
            {
                get; set;
            }

        }
        public class CreateProfileScore
        {
            public int ProfileType { get; set; }
            public string Name { get; set; }
            public int Score { get; set; }
            public IFormFile IconFile { get; set; }
        }
        public class EditProfileScore
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ProfileType { get; set; }
            public int Score { get; set; }
            public IFormFile IconFile { get; set; }
            public int IconId { get; set; }
            public string? IconName { get; set; }
        }

    }
}
