using System;

namespace Schedule.Domain.Entities
{
    public class Laboratory : Course
    {
        public string Group { get; private set; }
        public Teacher Teacher { get; private set; }


        public static Laboratory Create(string name, string group, Teacher teacher, string weekday, short starthour, short endhour) => new Laboratory
        {
            Id = Guid.NewGuid(),
            Name = name,
            Group = group,
            Teacher = teacher,
            Weekday = weekday,
            StartHour = starthour,
            EndHour = endhour
        };
    }
}
