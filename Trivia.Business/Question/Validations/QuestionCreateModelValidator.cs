using System;
using FluentValidation;

namespace Trivia.Business.Question.Validations
{
    class QuestionCreateModelValidator : AbstractValidator<QuestionCreateModel>
    {
        public QuestionCreateModelValidator()
        {
            RuleFor(t => t.TeacherId).NotEmpty();
            RuleFor(t => t.CourseId).NotEmpty();
            RuleFor(t => t.Text).NotEmpty().MinimumLength(5);
        }
    }
}
