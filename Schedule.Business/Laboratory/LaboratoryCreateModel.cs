using System.ComponentModel.DataAnnotations;
using Schedule.Domain.Entities;

namespace Schedule.Business.Laboratory
{
    public class LaboratoryCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Group { get; set; }

        [Required]
        public Teacher Teacher { get; set; }

        [Required]
        [MaxLength(50)]
        public string Weekday { get; set; }

        [Required]
        public short StartHour { get; set; }

        [Required]
        public short EndHour { get; set; }
    }
}
