using Application.Dtos.Common;
using Domain.Enums;

namespace Application.Features.Profile.Dtos
{
    public record GetProfileDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Username { get; init; }
        public string Bio { get; init; }
        public string Link { get; init; }
        public int Score { get; init; }
        public ProfileType ProfileType { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime BirthDate { get; init; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public int BlockedCount { get; set; }
        public int AdsCount { get; set; }
        public string PhoneNumber { get; init; }
        public string Email { get; init; }
        public GetNameAndIdString User { get; init; }
        public int? AvatarId { get; init; }
        public string? Avatar { get; init; }
    }

}
