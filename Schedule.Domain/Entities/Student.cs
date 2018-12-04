using System;
using System.Dynamic;

namespace Schedule.Domain.Entities
{
    public class Student : Entity
    {
        public string Group { get; set; }
        public short Year { get; set; }


        public static Student Create(string group, short year) => new Student()
        {
            Id = Guid.NewGuid(),
            Group = group,
            Year = year
        };
    }
}
