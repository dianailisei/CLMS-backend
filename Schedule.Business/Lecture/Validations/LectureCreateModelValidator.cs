using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Schedule.Business.Lecture.Validations
{
    class LectureCreateModelValidator : AbstractValidator<LectureCreateModel>
    {
        public LectureCreateModelValidator()
        {
            RuleFor(l => l.Name).NotEmpty().Length(2, 20);
            RuleFor(l => l.Weekday).NotEmpty();
            RuleFor(l => l.StartHour).NotEmpty();
            RuleFor(l => l.EndHour).NotEmpty();
            RuleFor(l => l.HalfYear).NotEmpty();
        }
    }
}
