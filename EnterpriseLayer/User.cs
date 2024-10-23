namespace EnterpriseLayer
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Un usuario pertenece a una persona
        public int PeopleId { get; set; }
        public People People { get; set; }

        public User(string id, string name, string email, People people) 
        {
            Id = id;
            Name = name;
            Email = email;
            People = people;
        }

    }

    public class People
    {
        public string DNI { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        // Una persona puede tener múltiples usuarios
        public List<User> Users { get; set; } = new List<User>();

        public People(string dni, string name, string lastName, string address)
        {
            DNI = dni;
            Name = name;
            LastName = lastName;
            Address = address;
        }
    }
}
