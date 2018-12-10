using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Schedule.Domain.Entities;

namespace Schedule.Business.Subject
{
    public class SubjectCreateModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Domain.Entities.Lecture> Lectures { get; private set; }

        public ICollection<Domain.Entities.Laboratory> Laboratories { get; private set; }
    }
}
