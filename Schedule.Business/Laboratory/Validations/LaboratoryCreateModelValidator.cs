using FluentValidation;


namespace Schedule.Business.Laboratory.Validations
{
    class LaboratoryCreateModelValidator: AbstractValidator<LaboratoryCreateModel>
    {
        public LaboratoryCreateModelValidator()
        {
            RuleFor(l => l.Name).NotEmpty().Length(2, 20);
            RuleFor(l => l.Group).NotEmpty().Length(2, 5);
            RuleFor(l => l.Weekday).NotEmpty().Length(2, 10);
            RuleFor(l => l.StartHour).NotEmpty();
            RuleFor(l => l.EndHour).NotEmpty();
        }
    }
}
