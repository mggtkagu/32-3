using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSR4.Entity;

namespace WSR4.Core
{
   public class User
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public User CreateFromPerson(Person person)
        {
            Id = person.Id;
            FirstName = person.FirstName;
            SecondName = person.SecondName;
            LastName = person.LastName;
            Login = person.Login;
            Password = person.Password;
            Role = person.Manager.Select(manager => manager.IdPerson).Contains(person.Id) ? Role.Manager: Role.Executor;
            return this;
        }

    }
}
