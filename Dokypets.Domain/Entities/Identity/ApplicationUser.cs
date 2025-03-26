using Microsoft.AspNetCore.Identity;

namespace Dokypets.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public string? UrlPhoto { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
