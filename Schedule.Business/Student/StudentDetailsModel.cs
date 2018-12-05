using System;

namespace Schedule.Business.Student
{
    public class StudentDetailsModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Group { get; set; }

        public short Year { get; set; }
    }
}
