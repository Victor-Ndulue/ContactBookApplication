using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
    
}
