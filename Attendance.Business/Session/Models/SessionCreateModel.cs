using System;

namespace Attendance.Business.Session.Models
{
    public class SessionCreateModel
    {
        public Guid LaboratoryId { set; get; }
        public short Duration { get; set; }
    }
}
