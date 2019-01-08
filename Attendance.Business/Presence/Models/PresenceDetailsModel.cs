using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Business.Presence.Models
{
    public class PresenceDetailsModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public DateTime SubmitDate { get; set; }
        public Domain.Session SessionEnrolled { get; set; }
    }
}
