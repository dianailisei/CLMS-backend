using System.ComponentModel.DataAnnotations;

namespace Schedule.Business.Lecture
{
    public class LectureCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Weekday { get; set; }

        [Required]
        public short StartHour { get; set; }

        [Required]
        public short EndHour { get; set; }

        [Required]
        public string HalfYear { get; set; }
    }
}
