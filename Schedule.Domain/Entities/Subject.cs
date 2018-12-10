using System;
using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Subject : Entity
    {
        public string Name { get; private set; }
        public Teacher HeadOfDepartment { get; private set; }
        public ICollection<Lecture> Lectures { get; private set; }
        public ICollection<Laboratory> Laboratories { get; private set; }

        public Subject()
        {
            Lectures = new List<Lecture>();
            Laboratories = new List<Laboratory>();
        }

        public static Subject Create(Teacher headOfDepartment, string name) => new Subject
        {
            Id = Guid.NewGuid(),
            Name = name,
            HeadOfDepartment = headOfDepartment
        };

        public void Update(string name)
        {
            Name = name;
        }
    }
}
