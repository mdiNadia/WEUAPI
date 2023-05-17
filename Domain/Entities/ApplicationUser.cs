using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? KnownAs { get; set; }
        public string NumCode { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }= DateTime.Now;

        public List<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Profile> Profiles { get; set; } 




        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }


    }
}
