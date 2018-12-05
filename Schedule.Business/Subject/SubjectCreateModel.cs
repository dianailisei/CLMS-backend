﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Schedule.Domain.Entities;

namespace Schedule.Business.Subject
{
    public class SubjectCreateModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public ICollection<Lecture> Lectures { get; private set; }

        [Required]
        public ICollection<Laboratory> Laboratories { get; private set; }
    }
}