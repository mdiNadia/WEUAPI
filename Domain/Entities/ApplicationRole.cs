using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
