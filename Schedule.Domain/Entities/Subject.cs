using System;
using System.Collections.Generic;


namespace Schedule.Domain.Entities
{
    public class Subject
    {
        public Guid SubjectId { get; private set; }
        public string Name { get; private set; }
        public ICollection<Lecture> Lectures { get; private set; }
        public ICollection<Laboratory> Laboratories { get; private set; }

        public static Subject Create(string name, ICollection<Lecture> lectures, ICollection<Laboratory> laboratories) => new Subject
        {
            SubjectId = new Guid(),
            Name = name,
            Lectures = lectures,
            Laboratories = laboratories
        };
    }
}
