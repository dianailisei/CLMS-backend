using System;
using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Lecture : Course
    {
        public string HalfYear { get; private set; }
        public Teacher Lecturer { get; private set; }
        public Subject ParentSubject { get; private set; }

        public static Lecture Create(string name, string weekday, short starthour, short endhour, string halfyear, Teacher lecturer, Subject parentSubject) => new Lecture
        {
            Id = Guid.NewGuid(),
            Name = name,
            Weekday = weekday,
            StartHour = starthour,
            EndHour = endhour,
            HalfYear = halfyear,
            Lecturer = lecturer,
            ParentSubject = parentSubject
        };

        public void Update(string name, string weekday, short starthour, short endhour, string halfyear, Teacher lecturer)
        {
            Name = name;
            Weekday = weekday;
            StartHour = starthour;
            EndHour = endhour;
            HalfYear = halfyear;
            Lecturer = lecturer;
        }
    }
}
