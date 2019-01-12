using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Business.Lecture
{
    public class LectureDetailsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Weekday { get; set; }
        public short StartHour { get; set; }
        public short EndHour { get; set; }
        public string HalfYear { get; set; }
        public Domain.Entities.Teacher Lecturer { get; set; }
        public Domain.Entities.Subject ParentSubject { get; set; }  
    }
}
