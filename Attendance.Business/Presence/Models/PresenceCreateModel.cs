using System;

namespace Attendance.Business.Presence.Models
{
    public class PresenceCreateModel
    {
        public Guid StudentId { get; set; }
        public string ConfirmationCode { get; set; }
    }
}
