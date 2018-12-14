using FluentValidation;

namespace Schedule.Business.Teacher.Validations
{
    class TeacherCreateModelValidator : AbstractValidator<TeacherCreateModel>
    {
        public TeacherCreateModelValidator()
        {
            RuleFor(t => t.FirstName).NotEmpty().Length(2, 20);
            RuleFor(t => t.LastName).NotEmpty().Length(2, 20);
            RuleFor(t => t.Email).NotEmpty().EmailAddress();
            RuleFor(t => t.Password).NotEmpty().MinimumLength(6);
        }
    }
}
