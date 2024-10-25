namespace Models
{
    public class PeopleModel
    {
        public int Id { get; set; }
        public string DNI { get; set; }
        public string Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        // Nueva colección de AppUsers
        public virtual ICollection<UserModel> Users { get; set; } = new List<UserModel>();
        public virtual ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();

    }
}
