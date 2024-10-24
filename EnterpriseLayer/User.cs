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
}
