using System;

namespace Schedule.Domain.Entities
{
    public class Student : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Group { get; private set; }
        public short Year { get; private set; }


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
    }
}
