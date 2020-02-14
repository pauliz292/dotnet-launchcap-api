using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual AppUser User { get; set; }
        public virtual Role Role { get; set; }
    }
}