using System;

namespace Schedule.Domain.Entities
{
    abstract class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Weekday { get; set; }
        public short StartHour { get; set; }
        public short EndHour { get; set; }
    }
}
