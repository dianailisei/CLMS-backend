using System.ComponentModel.DataAnnotations;

namespace Schedule.Business.Lecture
{
    public class LectureCreateModel
    {
        public string Name { get; set; }

        public string Weekday { get; set; }

        public short StartHour { get; set; }

        public short EndHour { get; set; }

        public string HalfYear { get; set; }
    }
}
