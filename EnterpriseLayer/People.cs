using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLayer
{
    public class People
    {
        public int Id { get; set; }
        public string DNI { get; set; }
        public string Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        // Una persona puede tener múltiples usuarios
        public List<User> Users { get; set; } = new List<User>();

        public People(string dni, string name, string lastName, string address)
        {
            DNI = dni;
            Name = name;
            LastName = lastName;
            Address = address;
        }

        public People(int id, string dni, string name, string lastName, string address)
        {
            Id = id;
            DNI = dni;
            Name = name;
            LastName = lastName;
            Address = address;
        }
    }
}
