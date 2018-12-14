using System.ComponentModel.DataAnnotations;

namespace Schedule.Business.Teacher
{
    public class TeacherCreateModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
