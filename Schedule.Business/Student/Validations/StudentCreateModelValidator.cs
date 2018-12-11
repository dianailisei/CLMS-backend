using FluentValidation;

namespace Schedule.Business.Student.Validations
{
    public class StudentCreateModelValidator : AbstractValidator<StudentCreateModel>
    {
        public StudentCreateModelValidator()
        {
            RuleFor(s => s.LastName).NotEmpty().Length(2,20);
            RuleFor(s=> s.FirstName).NotEmpty().Length(2,20);
            RuleFor(s => s.Email).NotEmpty().EmailAddress();
            RuleFor(s => s.Year).InclusiveBetween((short)1,(short)3);
            RuleFor(s => s.Group).NotEmpty().Length(2,2).Matches(@"^[A-Z][1-7]$");
            RuleFor(s => s.Password).NotEmpty().MinimumLength(6);
        }
    }
}
