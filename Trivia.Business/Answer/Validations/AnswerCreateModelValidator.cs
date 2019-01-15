using FluentValidation;

namespace Trivia.Business.Answer.Validations
{
    public class AnswerCreateModelValidator : AbstractValidator<AnswerCreateModel>
    {
        public AnswerCreateModelValidator()
        {
            RuleFor(t => t.StudentId).NotEmpty();
            RuleFor(t => t.QuestionId).NotEmpty();
            RuleFor(t => t.Text).NotEmpty();
        }
    }
}
