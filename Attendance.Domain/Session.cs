using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Domain
{
    public class Session : Entity
    {
        [Required]
        public Guid LaboratoryId { get; private set; }
        [Required]
        public string ConfirmationCode { get; private set; }
        [Required]
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public ICollection<Presence> Presences { get; private set; }

        private Session()
        {
            Presences = new List<Presence>();
        }

        public static Session Create(Guid laboratoryId, string confrimationCode, int duration)
            => new Session()
            {
                Id = Guid.NewGuid(),
                LaboratoryId = laboratoryId,
                ConfirmationCode = confrimationCode,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(duration)
            };

        public void Update(string confirmationCode, int duration)
        {
            ConfirmationCode = confirmationCode;
            EndTime = DateTime.Now.AddMinutes(duration);
        }

        public void Delete()
        {
            Available = false;
        }

    }
}
