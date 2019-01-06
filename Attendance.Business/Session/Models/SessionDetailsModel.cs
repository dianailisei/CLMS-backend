using System;
using System.Collections.Generic;

namespace Attendance.Business.Session.Models
{
    public class SessionDetailsModel
    {
        public Guid Id { get; set; }
        public Guid LaboratoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string ConfirmationCode { get; set; }

        public ICollection<Domain.Presence> Presences { get; set; }
    }
}
