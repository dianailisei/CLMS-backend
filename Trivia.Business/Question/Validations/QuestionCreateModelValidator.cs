using System;
using FluentValidation;

namespace Trivia.Business.Question.Validations
{
    class QuestionCreateModelValidator : AbstractValidator<QuestionCreateModel>
    {
        public QuestionCreateModelValidator()
        {
            RuleFor(q => q.TeacherId).NotEmpty();
            RuleFor(q => q.CourseId).NotEmpty();
            RuleFor(q => q.Duration).NotNull().Must(d => d > 0 && d <= 60);
            RuleFor(q => q.Points).NotNull().Must(p => p > 0 && p < 10);
            RuleFor(q => q.Text).NotEmpty().MinimumLength(5);
        }
    }
}
