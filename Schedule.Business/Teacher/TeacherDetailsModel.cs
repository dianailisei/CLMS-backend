using System;
using System.Collections.Generic;
using Schedule.Domain.Entities;

namespace Schedule.Business.Teacher
{
    public class TeacherDetailsModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Domain.Entities.Subject> Subjects { get; set; }
    }
}
