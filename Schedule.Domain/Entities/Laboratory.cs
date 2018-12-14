using System;

namespace Schedule.Domain.Entities
{
    public class Laboratory : Course
    {
        public string Group { get; private set; }
        public Teacher Teacher { get; private set; }
        public Subject ParentSubject { get; private set; }

        public static Laboratory Create(string name, string group, Teacher teacher, string weekday, short starthour, short endhour, Subject parentSubject) => new Laboratory
        {
            Id = Guid.NewGuid(),
            Name = name,
            Group = group,
            Teacher = teacher,
            Weekday = weekday,
            StartHour = starthour,
            EndHour = endhour,
            ParentSubject = parentSubject
        };

        public void Update(string name, string group, Teacher teacher, string weekday, short starthour, short endhour)
        {
            Name = name;
            Group = group;
            Teacher = teacher;
            Weekday = weekday;
            StartHour = starthour;
            EndHour = endhour;
            
        }

        public void Delete()
        {
            Available = false;
        }
    }
}
