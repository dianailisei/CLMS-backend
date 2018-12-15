using System;

namespace Schedule.Domain.Entities
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Group { get; set; }
        public short Year { get; set; }

        // Validare in metode
        public static Student Create(string firstName, string lastName,
            string email, string pass, string group, short year) => new Student()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = pass,
            Group = group,
            Year = year
        };

        public void Update(string firstName, string lastName,
            string email, string pass, string group, short year)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = pass;
            Group = group;
            Year = year;
        }
    }
}
