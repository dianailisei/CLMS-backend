using System;
using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Lecture : Course
    {
        public string HalfYear { get; private set; }
        public ICollection<Teacher> Teachers { get; private set; }

        public static Lecture Create(string name, string weekday, short starthour, short endhour) => new Lecture
        {
            Id = new Guid(),
            Name = name,
            Weekday = weekday,
            StartHour = starthour,
            EndHour = endhour
        };
    }
}
