namespace Application.Dtos.Profile
{
    public class ProfileDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public int AvatarId { get; set; }
        public string Bio { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public bool IsFollowed { get; set; }

    }
}
