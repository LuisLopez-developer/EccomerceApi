using Microsoft.AspNetCore.Identity;

namespace EccomerceApi.Entity
{
    public class AppUser : IdentityUser
    {

        public int? StateId { get; set; }
        public virtual State State { get; set; }

        // Nueva propiedad para relacionar con People
        public int PeopleId { get; set; }
        public virtual People People { get; set; }
    }
}
