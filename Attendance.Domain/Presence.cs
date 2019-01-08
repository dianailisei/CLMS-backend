using System;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Domain
{
    public class Presence : Entity
    {
        [Required]
        public Guid StudentId { get; private set; }

        public Session SessionEnrolled { get; private set; }

        public DateTime SubmitDate { get; private set; }

        public static Presence Create(Guid studId, Session session) => new Presence()
        {
            Id = Guid.NewGuid(),
            StudentId = studId,
            SessionEnrolled = session,
            SubmitDate = DateTime.Now
        };

        public void Delete()
        {
            Available = false;
        }
    }
}
