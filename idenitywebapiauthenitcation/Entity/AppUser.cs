using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Entity
{
    public class AppUser : IdentityUser
    {

        public required int StateId { get; set; }
        public virtual State State { get; set; }

        // Nueva propiedad para relacionar con People
        public int PeopleId { get; set; }
        public virtual People People { get; set; }
    }

}
