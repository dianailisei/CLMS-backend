﻿using Schedule.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Schedule.Business.Subject
{
    public class SubjectDetailsModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public short Year { get; set; }

        public Domain.Entities.Teacher HeadOfDepartment { get; set; }

        public ICollection<Domain.Entities.Lecture> Lectures { get; set; }

        public ICollection<Domain.Entities.Laboratory> Laboratories { get; set; }
    }
}
