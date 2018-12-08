using System;
using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Lecture : Course
    {
        private Lecture() => Teachers = new List<Teacher>();
        public string HalfYear { get; private set; }
        public ICollection<Teacher> Teachers { get; private set; }

        public static Lecture Create(string name, string weekday, short starthour, short endhour, string halfyear) => new Lecture
        {
            Id = Guid.NewGuid(),
            Name = name,
            Weekday = weekday,
            StartHour = starthour,
            EndHour = endhour,
            HalfYear = halfyear
        };

        public void Update(string name, string weekday, short starthour, short endhour, string halfyear)
        {
            Name = name;
            Weekday = weekday;
            StartHour = starthour;
            EndHour = endhour;
            HalfYear = halfyear;
        }
    }
}
