using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class UserModel : IdentityUser
    {

        public required int StateId { get; set; }
        public virtual StateModel State { get; set; }

        // Nueva propiedad para relacionar con People
        public int PeopleId { get; set; }
        public virtual PeopleModel People { get; set; }
    }

}
