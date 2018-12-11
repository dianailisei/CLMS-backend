using System.ComponentModel.DataAnnotations;

namespace Schedule.Business.Student
{
    public class StudentCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Group { get; set; }

        [Required]
        public short Year { get; set; }
    }
}
