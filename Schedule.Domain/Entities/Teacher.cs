using System;
using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Teacher : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public ICollection<Subject> Subjects { get; private set; }

        public static Teacher Create(string firstName, string lastName, string email, string password) => new Teacher()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            Subjects = new List<Subject>()
        };
    }
}
