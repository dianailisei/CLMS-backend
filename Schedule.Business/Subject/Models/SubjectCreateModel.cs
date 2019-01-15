using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Schedule.Domain.Entities;

namespace Schedule.Business.Subject
{
    public class SubjectCreateModel
    {
        public string Name { get; set; }
        public short Year { get; set; }
    }
}
