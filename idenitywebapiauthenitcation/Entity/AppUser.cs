using Microsoft.AspNetCore.Identity;

namespace EccomerceApi.Entity
{
    public class AppUser : IdentityUser
    {
        public int? StateId { get; set; }
        public virtual State State { get; set; }
    }
}
