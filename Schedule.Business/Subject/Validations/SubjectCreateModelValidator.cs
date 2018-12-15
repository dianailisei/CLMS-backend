using FluentValidation;

namespace Schedule.Business.Subject.Validations
{
    class SubjectCreateModelValidator : AbstractValidator<SubjectCreateModel>
    {
        public SubjectCreateModelValidator()
        {
            RuleFor(s => s.Name).NotEmpty().Length(2, 20);
        }
    }
}
