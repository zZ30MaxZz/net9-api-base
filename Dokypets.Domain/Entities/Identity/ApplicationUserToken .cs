using Microsoft.AspNetCore.Identity;

namespace Dokypets.Domain.Entities.Identity
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
    }
}
