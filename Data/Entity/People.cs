namespace Data.Entity
{
    public class People
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        // Nueva colección de AppUsers
        public virtual ICollection<AppUser> AppUsers { get; set; } = [];

    }
}
