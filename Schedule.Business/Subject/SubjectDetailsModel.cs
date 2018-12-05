﻿using Schedule.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Schedule.Business.Subject
{
    public class SubjectDetailsModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Lecture> Lectures { get; set; }

        public ICollection<Laboratory> Laboratories { get; set; }
    }
}
