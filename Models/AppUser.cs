using Microsoft.AspNetCore.Identity;

namespace Models
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
